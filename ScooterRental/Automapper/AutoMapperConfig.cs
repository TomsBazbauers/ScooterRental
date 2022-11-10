using AutoMapper;
using ScooterRental.Core.Models;
using ScooterRental.Models;

namespace ScooterRental.Automapper
{
    public class AutoMapperConfig
    {
        public static IMapper CreateMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ScooterRequest, Scooter>()
                .ForMember(s => s.Id, options => options.Ignore())
                .ForMember(x => x.PricePerMinute, opt => opt.MapFrom(s => s.PricePerMinute));

                cfg.CreateMap<Scooter, ScooterRequest>()
                .ForMember(x => x.PricePerMinute, opt => opt.MapFrom(s => s.PricePerMinute));
            });

            config.AssertConfigurationIsValid();

            return config.CreateMapper();
        }
    }
}