namespace FuelAcc.Application.UseCases.Commons.Exceptions
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}