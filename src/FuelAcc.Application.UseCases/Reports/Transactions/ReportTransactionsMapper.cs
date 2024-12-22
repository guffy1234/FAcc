using AutoMapper;
using FuelAcc.Domain.Entities.ReportingModels;

namespace FuelAcc.Application.UseCases.Reports.Transactions
{
    public class ReportTransactionsMapper : Profile
    {
        public ReportTransactionsMapper()
        {
            CreateMap<TransactionReport, ReportTransactionView>().ReverseMap();
        }
    }
}