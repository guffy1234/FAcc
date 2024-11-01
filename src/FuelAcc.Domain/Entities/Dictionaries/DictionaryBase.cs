using FuelAcc.Domain.Commons;

namespace FuelAcc.Domain.Entities.Dictionaries
{
    public class DictionaryBase : ChangesInfo, IDictionaryEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}