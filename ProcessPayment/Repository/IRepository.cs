using ProcessPayment.Services;
using ProcessPayment.Services.CheapPaymentGateway;
using ProcessPayment.Services.PremiumPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Repository
{
  public interface IRepository
  {
    ICheapPaymentGateway ICheapPaymentGateway { get; set; }
    IExpensivePaymentGateway IExpensivePaymentGateway { get; set; }
    IPremiumPaymentService IPremiumPaymentService { get; set; }
    
  }
}
