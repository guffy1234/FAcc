namespace FuelAcc.Application.UseCases.Commons.Commands
{
    public record DeleteCommand<DTO>(Guid Id) : Command;
}