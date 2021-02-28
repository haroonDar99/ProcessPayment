using ProcessPayment.DataLayer;
using ProcessPayment.ViewModels;
using System.Threading.Tasks;

namespace ProcessPayment.Services.CheapPaymentGateway
{
  public interface ICheapPaymentGateway
  {
    Task<bool> ProcessPayments(PaymentData reqObj, PaymentsDbContext context);
  }
}
