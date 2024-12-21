using AutoMapper;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.Domain.Entities.Documents;

namespace FuelAcc.Application.UseCases.Replication
{
    public class ReplicationMapper : Profile
    {
        public ReplicationMapper()
        {
            CreateMap<OrderLine, OrderLineDto>().ReverseMap();
            CreateMap<OrderPropertyLine, OrderPropertyDto>().ReverseMap();
        }
    }
}