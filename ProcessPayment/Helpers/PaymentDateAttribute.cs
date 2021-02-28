using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Helpers
{
  public class PaymentDateAttribute : ValidationAttribute
  {
    public override bool IsValid(object value)// Return a boolean value: true == IsValid, false != IsValid
    {
      DateTime d = Convert.ToDateTime(value);
      return d >= DateTime.Now; //Dates Greater than or equal to today are valid (true)
    }
  }
}
