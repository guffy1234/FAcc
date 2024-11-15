using AutoMapper;
using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.UseCases.Reports.Rests
{
    public class ReportRestsMapper : Profile
    {
        public ReportRestsMapper()
        {
            CreateMap<Rest, ReportRestView>().ReverseMap();
        }
    }
}