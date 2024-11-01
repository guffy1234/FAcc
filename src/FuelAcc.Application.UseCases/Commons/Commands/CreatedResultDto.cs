namespace FuelAcc.Application.UseCases.Commons.Commands
{
    public class CreatedResultDto
    {
        public Guid Id { get; set; }

        public CreatedResultDto(Guid id)
        {
            Id = id;
        }
    }
}