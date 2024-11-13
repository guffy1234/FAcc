using AutoMapper;
using FuelAcc.Application.Dto.Dictionaries;
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