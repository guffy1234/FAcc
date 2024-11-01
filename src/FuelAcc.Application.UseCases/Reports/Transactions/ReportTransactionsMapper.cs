using AutoMapper;
using FuelAcc.Domain.Entities.Registry;

namespace FuelAcc.Application.UseCases.Reports.Transactions
{
    public class ReportTransactionsMapper : Profile
    {
        public ReportTransactionsMapper()
        {
            CreateMap<Transaction, ReportTransactionView>().ReverseMap();
        }
    }
}