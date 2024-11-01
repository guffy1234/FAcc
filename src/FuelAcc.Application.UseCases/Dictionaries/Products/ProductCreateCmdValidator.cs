using FluentValidation;
using FuelAcc.Application.UseCases.Commons.Commands;
using FuelAcc.Application.UseCases.Products;

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