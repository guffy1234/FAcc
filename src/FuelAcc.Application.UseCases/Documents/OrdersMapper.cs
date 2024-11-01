using AutoMapper;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents
{
    public class OrdersMapper : Profile
    {
        public OrdersMapper()
        {
            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
        }
    }
}