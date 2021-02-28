using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Polly;
using Polly.Retry;
using ProcessPayment.DataLayer;
using ProcessPayment.Repository;
using ProcessPayment.ViewModels;
using System;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static ProcessPayment.Helpers.GlobalEnums;

namespace ProcessPayment.Controllers
{
  [Route("api/[controller]/[action]")]
  [ApiController]
  public class PaymentsController : ControllerBase
  {
    private const int MaxRetries = 3;
    private Regex expression = new Regex(@"^(?:4[0-9]{12}(?:[0-9]{3})?|5[1-5][0-9]{14}|6(?:011|5[0-9][0-9])[0-9]{12}|3[47][0-9]{13}|3(?:0[0-5]|[68][0-9])[0-9]{11}|(?:2131|1800|35\d{3})\d{11})$");
    private readonly IRepository _repository;
    private readonly IMapper _mapper;
    private readonly PaymentsDbContext _context;
    private readonly AsyncRetryPolicy _retryPolicy;
    
    public PaymentsController(PaymentsDbContext context, IRepository repository, IMapper mapper)
    {
      _context = context;
      _repository = repository;
      _mapper = mapper;
      _retryPolicy = Policy.Handle<Exception>().RetryAsync(retryCount: MaxRetries);
    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> ProcessPayment(PaymentDataViewModel requestObj)
    {
      try
      {
        if (string.IsNullOrEmpty(requestObj.CreditCardNumber) && !expression.IsMatch(requestObj.CreditCardNumber))
          return BadRequest("CreditCardNumber Is Invalid");
        else if (string.IsNullOrEmpty(requestObj.CardHolder))
          return BadRequest("CardHolder Is Invalid");
        else if (requestObj.SecurityCode != null && requestObj.SecurityCode.Length > 0 && requestObj.SecurityCode.Length > 3)
          return BadRequest("SecurityCode Is Invalid");
        else if (requestObj.ExpirationDate == null)
          return BadRequest("ExpirationDate Is Invalid");
        else if (requestObj.Amount <= 0)
          return BadRequest("Amount Is Invalid");

        #region Apply Business Rules
        bool response = false;
        var model = _mapper.Map<PaymentData>(requestObj);


        #region CheapPaymentGateway
        if (requestObj.Amount <= 20)
        {
          response = await _repository.ICheapPaymentGateway.ProcessPayments(model,_context);
        } 
        #endregion

        #region ExpensivePaymentGateway
        else if (requestObj.Amount > 20 && requestObj.Amount <= 500)
        {
          response = await _repository.IExpensivePaymentGateway.ProcessPayments(model, _context);
          if(!response)
          {
            response = await _repository.ICheapPaymentGateway.ProcessPayments(model, _context);
          }
        } 
        #endregion

        #region PremiumPaymentService
        else if (requestObj.Amount > 500)
        {
          await _retryPolicy.ExecuteAsync(async () => {
            response = await _repository.IPremiumPaymentService.ProcessPayments(model, _context);
          });
        } 
        #endregion
        #endregion
        
        return Ok(response);
      }
      catch (Exception ex)
      {
        return StatusCode(int.Parse(HttpStatusCode.InternalServerError.ToString()));
      }
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> GetPaymentsData()
    {
      var response = await Task.Run(()=> _context.PaymentData.Include(i=> i.PaymentState).ToList());
      return Ok(response);
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> UpdatePaymentStatus(int paymentId)
    {
      try
      {
        if (paymentId == 0)
          return BadRequest("PaymentId Is Invalid");

        var response = await Task.Run(() => _context.PaymentData.Where(p => p.PaymentId == paymentId).Include(i => i.PaymentState).FirstOrDefault());

        if (response != null)
        {
          var paymentState = await Task.Run(() => _context.PaymentState.ToList());
          int paymnetstateId = Convert.ToInt32(PaymentStates.processed);
          response.PaymentState = paymentState.ToList().Where(s => s.PaymentStateId == paymnetstateId).FirstOrDefault();

          _context.Entry(response).State = EntityState.Modified;
          await _context.SaveChangesAsync();

          return Ok(response);
        }
      }
      catch (Exception)
      {
        return StatusCode(int.Parse(HttpStatusCode.InternalServerError.ToString()));
      }
      return Ok("Data Not Found");
    }


  }
}
