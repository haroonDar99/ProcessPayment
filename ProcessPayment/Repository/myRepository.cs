using ProcessPayment.Services;
using ProcessPayment.Services.CheapPaymentGateway;
using ProcessPayment.Services.ExpensivePaymentGateway;
using ProcessPayment.Services.PremiumPaymentService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Repository
{
  public class myRepository : IRepository
  {
    public ICheapPaymentGateway ICheapPaymentGateway { get => new CheapPaymentGateway(); set => new CheapPaymentGateway(); }
    public IExpensivePaymentGateway IExpensivePaymentGateway { get => new ExpensivePaymentGateway(); set => new ExpensivePaymentGateway(); }
    public IPremiumPaymentService IPremiumPaymentService { get => new PremiumPaymentService(); set => new PremiumPaymentService(); }
  }
}
