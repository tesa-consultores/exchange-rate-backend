using AutoMapper;
using BCP.ExchangeRate.Domain;
using BCP.ExchangeRate.WebAPI.Models.Request;

namespace BCP.ExchangeRate.WebAPI.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {

            CreateMap<AuthRequest, User>()
                .ForMember(dest => dest.Username, source => source.MapFrom(source => source.Username))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(source => source.Username));

        }
    }
}
