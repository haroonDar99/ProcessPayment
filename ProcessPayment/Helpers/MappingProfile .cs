using AutoMapper;
using ProcessPayment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProcessPayment.Helpers
{
  public class MappingProfile : Profile
  {
    public MappingProfile()
    {
      // Add as many of these lines as you need to map your objects
      CreateMap<PaymentDataViewModel, PaymentData>()
        .ForMember(dest=> dest.CreditCardNumber, opt=> opt.MapFrom(src=> src.CreditCardNumber))
        .ForMember(dest=> dest.CardHolder, opt=> opt.MapFrom(src=> src.CardHolder))
        .ForMember(dest=> dest.ExpirationDate, opt=> opt.MapFrom(src=> src.ExpirationDate))
        .ForMember(dest=> dest.SecurityCode, opt=> opt.MapFrom(src=> src.SecurityCode))
        .ForMember(dest=> dest.Amount, opt=> opt.MapFrom(src=> src.Amount));
    }
  }
}
