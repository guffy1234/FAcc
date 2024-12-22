using AutoMapper;
using FuelAcc.Domain.Entities.ReportingModels;

namespace FuelAcc.Application.UseCases.Reports.Rests
{
    public class ReportRestsMapper : Profile
    {
        public ReportRestsMapper()
        {
            CreateMap<RestReport, ReportRestView>().ReverseMap();
        }
    }
}