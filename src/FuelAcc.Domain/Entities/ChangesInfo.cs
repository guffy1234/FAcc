using System.Text.Json.Serialization;

namespace FuelAcc.Domain.Entities
{
    public abstract class ChangesInfo : ISoftDeleted
    {
        [JsonIgnore]
        public Guid CreatorUserId { get; set; }

        [JsonIgnore]
        public Guid? ModifierUserId { get; set; }

        [JsonIgnore]
        public DateTime Created { get; set; } = DateTime.UtcNow;

        [JsonIgnore]
        public DateTime? Modified { get; set; }

        [JsonIgnore]
        public bool IsDeleted { get; set; }
    }
}