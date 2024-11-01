using AutoMapper;
using FuelAcc.Application.UseCases.Products;
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