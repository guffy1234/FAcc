namespace FuelAcc.Application.UseCases.Commons.Events
{
    public record EventBase(bool IsInRepliactionContext) : Event;
}