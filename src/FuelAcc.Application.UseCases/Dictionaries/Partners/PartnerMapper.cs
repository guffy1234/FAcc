using AutoMapper;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Dictionaries.Partners
{
    public class PartnerMapper : Profile
    {
        public PartnerMapper()
        {
            CreateMap<Product, ProductDto>().ReverseMap();
        }
    }
}