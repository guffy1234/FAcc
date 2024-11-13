using FluentValidation;
using FuelAcc.Application.Dto.Dictionaries;
using FuelAcc.Application.UseCases.Commons.Commands;

namespace FuelAcc.Application.UseCases.Dictionaries.Products
{
    public class ProductCreateCmdValidator : AbstractValidator<CreateCommand<ProductDto>>
    {
        public ProductCreateCmdValidator()
        {
            RuleFor(x => x.Dto.Name).NotEmpty().NotNull();
        }
    }
}