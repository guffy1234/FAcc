using FluentValidation;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Application.UseCases.Commons.Commands;

namespace FuelAcc.Application.UseCases.Dictionaries.Partners
{
    public class PartnerCreateCmdValidator : AbstractValidator<CreateCommand<PartnerDto>>
    {
        public PartnerCreateCmdValidator()
        {
            RuleFor(x => x.Dto.Name).NotEmpty().NotNull();
            //RuleFor(x => x.Dto.Phone).NotEmpty().NotNull();
            //RuleFor(x => x.Dto.Region).NotEmpty().NotNull();
        }
    }
}