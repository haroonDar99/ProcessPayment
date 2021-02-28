using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Helpers
{
  public class GlobalEnums
  {
    public enum PaymentStates
    {
      pending = 1,
      processed = 2,
      failed = 3
    }
  }
}
