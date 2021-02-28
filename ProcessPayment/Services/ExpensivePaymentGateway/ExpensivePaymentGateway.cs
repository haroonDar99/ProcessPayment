using ProcessPayment.DataLayer;
using ProcessPayment.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using static ProcessPayment.Helpers.GlobalEnums;

namespace ProcessPayment.Services.ExpensivePaymentGateway
{
  public class ExpensivePaymentGateway : IExpensivePaymentGateway
  {
    public async Task<bool> ProcessPayments(PaymentData reqObj, PaymentsDbContext context)
    {
      try
      {
        var paymentState = await Task.Run(() => context.PaymentState.ToList());
        reqObj.PaymentState = new PaymentState();
        int paymnetstateId = Convert.ToInt32(PaymentStates.pending);
        reqObj.PaymentState = paymentState.ToList().Where(s => s.PaymentStateId == paymnetstateId).FirstOrDefault();

        context.PaymentData.Add(reqObj);
        await context.SaveChangesAsync();
        return true;
      }
      catch (Exception ex)
      {
        throw;
      }
    }
  }
}