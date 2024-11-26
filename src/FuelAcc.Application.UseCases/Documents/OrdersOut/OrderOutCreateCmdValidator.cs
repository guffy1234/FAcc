using FluentValidation;
using FuelAcc.Application.Dto.Documents;
using FuelAcc.Application.UseCases.Commons.Commands;

namespace FuelAcc.Application.UseCases.Documents.OrdersOut
{
    public class OrderOutCreateCmdValidator : AbstractValidator<CreateCommand<OrderOutDto>>
    {
        public OrderOutCreateCmdValidator()
        {
            RuleFor(x => x.Dto.Number).NotEmpty().NotNull();
            RuleFor(x => x.Dto.PartnerId).NotEmpty();
            RuleFor(x => x.Dto.FromStorageId).NotEmpty();
            RuleFor(x => x.Dto.Lines).NotEmpty().NotNull();
            RuleForEach(x => x.Dto.Lines).NotEmpty()
                .NotNull()
                .Must(r => r.Quantity > 0).WithMessage("Must be greater then 0")
                .Must(r => r.ProductId != Guid.Empty).WithMessage("Must have productId");
        }
    }
}