using AutoMapper;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Documents.OrdersMove
{
    public class OrderMoveMapper : Profile
    {
        public OrderMoveMapper()
        {
            CreateMap<OrderMove, OrderMoveDto>().ReverseMap();
        }
    }
}