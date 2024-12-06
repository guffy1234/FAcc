using AutoMapper;
using FuelAcc.Application.Dto.Other;
using FuelAcc.Application.Dto.Replication;
using FuelAcc.Domain.Entities.Other;
using FuelAcc.Domain.Entities.ReportingModels;

namespace FuelAcc.Application.UseCases.Documents
{
    public class OrdersMapper : Profile
    {
        public OrdersMapper()
        {
            CreateMap<PersistEvent, EventDto>().ReverseMap();
            CreateMap<Settings, SettingsDto>().ReverseMap();
            CreateMap<ReplictionPacket, ReplictionPacketDto>().ReverseMap();
            CreateMap<ReplictionPacketView, ReplictionPacketViewDto>().ReverseMap();
        }
    }
}