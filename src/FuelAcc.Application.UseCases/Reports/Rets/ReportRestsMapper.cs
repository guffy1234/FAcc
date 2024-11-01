using AutoMapper;
using FuelAcc.Application.UseCases.Reports.Rets;
using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.UseCases.Reports.Transactions
{
    public class ReportRestsMapper : Profile
    {
        public ReportRestsMapper()
        {
            CreateMap<Rest, ReportRestView>().ReverseMap();
        }
    }
}