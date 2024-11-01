namespace FuelAcc.Application.UseCases.Commons.Commands
{
    public record CreateCommand<DTO>(DTO Dto) : Command;
}