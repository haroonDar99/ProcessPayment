using Microsoft.EntityFrameworkCore;
using ProcessPayment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.DataLayer
{
  public class PaymentsDbContext : DbContext
  {
   
    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options)
    {
    }

    public DbSet<PaymentData> PaymentData { get; set; }
    public DbSet<PaymentState> PaymentState { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<PaymentState>()
          .HasData(
              new PaymentState
              {
                PaymentStateId = 1,
                PaymentStateDescription = "pending"
              },
              new PaymentState
              {
                PaymentStateId = 2,
                PaymentStateDescription = "processed"
              },
              new PaymentState
              {
                PaymentStateId = 3,
                PaymentStateDescription = "failed"
              }
          );
    }

  }
}
