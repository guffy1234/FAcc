namespace FuelAcc.Application.UseCases.Commons.Commands
{
    public record UpdateCommand<DTO>(DTO Dto) : Command;
}