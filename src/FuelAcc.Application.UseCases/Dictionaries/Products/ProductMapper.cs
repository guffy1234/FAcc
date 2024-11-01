using AutoMapper;
using FuelAcc.Application.UseCases.Dictionaries.Partners;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Products
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateMap<Partner, PartnerDto>().ReverseMap();
        }
    }
}