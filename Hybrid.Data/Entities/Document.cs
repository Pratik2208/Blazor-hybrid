using System.Text.Json.Serialization;

namespace Hybrid.Data.Entities
{
    public abstract class Document
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        protected Document()
        {
            Id = Guid.NewGuid();
        }
    }
}
