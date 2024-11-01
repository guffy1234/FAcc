using AutoMapper;
using FuelAcc.Domain.Entities.Dictionaries;

namespace FuelAcc.Application.UseCases.Dictionaries.Branches
{
    public class BranchMapper : Profile
    {
        public BranchMapper()
        {
            CreateMap<Branch, BranchDto>().ReverseMap();
        }
    }
}