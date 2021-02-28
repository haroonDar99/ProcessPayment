using ProcessPayment.DataLayer;
using ProcessPayment.ViewModels;
using System.Threading.Tasks;

namespace ProcessPayment.Services.PremiumPaymentService
{
  public interface IPremiumPaymentService
  {
    Task<bool> ProcessPayments(PaymentData reqObj, PaymentsDbContext context);
  }
}
