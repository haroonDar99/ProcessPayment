using ProcessPayment.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.ViewModels
{
  public class PaymentData
  {
    [Column("PaymentId")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int PaymentId { get; set; }

    [Column("CreditCardNumber")]
    [Required]
    [StringLength(19)]
    public string CreditCardNumber { get; set; }

    [Column("CardHolder")]
    [Required]
    [StringLength(50)]
    public string CardHolder { get; set; }

    [Column("ExpirationDate")]
    [Required]
    [PaymentDateAttribute(ErrorMessage = "Invalid date")]
    public DateTime ExpirationDate { get; set; }

    [Column("SecurityCode")]
    [StringLength(3)]
    public string SecurityCode { get; set; }

    [Column("Amount")]
    [Required]
    [Range(0.0, Double.MaxValue, ErrorMessage = "The field {0} must be greater than {1}.")]
    public decimal Amount { get; set; }

    [ForeignKey("PaymentStateId")]
    public virtual PaymentState PaymentState { get; set; }

  }
}
