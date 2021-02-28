using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.ViewModels
{
  public class PaymentState
  {
    [Column("PaymentStateId")]
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int PaymentStateId { get; set; }

    [Column("PaymentStateDescription")]
    [Required]
    [StringLength(15)]
    public string PaymentStateDescription { get; set; }
  }
}
