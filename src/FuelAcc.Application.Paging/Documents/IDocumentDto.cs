namespace FuelAcc.Application.DtoCommon.Documents
{
    public interface IDocumentDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Number { get; set; }
    }
}