using AutoMapper;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Domain.Entities.Other;

namespace FuelAcc.Application.UseCases.Documents
{
    public class OrdersMapper : Profile
    {
        public OrdersMapper()
        {
            CreateMap<PersistEvent, EventDto>().ReverseMap();
            CreateMap<ReplictionPacket, ReplictionPacketDto>().ReverseMap();
        }
    }
}