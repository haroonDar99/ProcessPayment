using ProcessPayment.DataLayer;
using ProcessPayment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Services
{
  public interface IExpensivePaymentGateway
  {
    Task<bool> ProcessPayments(PaymentData reqObj, PaymentsDbContext context);
  }
}
