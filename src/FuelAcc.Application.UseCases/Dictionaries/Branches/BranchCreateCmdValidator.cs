using FluentValidation;
using FuelAcc.Application.UseCases.Commons.Commands;

namespace FuelAcc.Application.UseCases.Dictionaries.Branches
{
    public class BranchCreateCmdValidator : AbstractValidator<CreateCommand<BranchDto>>
    {
        public BranchCreateCmdValidator()
        {
            RuleFor(x => x.Dto.Name).NotEmpty().NotNull();
        }
    }
}