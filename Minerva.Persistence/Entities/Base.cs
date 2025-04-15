namespace Minerva.Persistence.Entities
{
    public class BaseEntity
    {
        public int Id { get; set; }

        // Raw JSON string, stored in DB
        public string? MetaDataJson { get; set; }

        // Optional: compressed or binary serialized
        public byte[]? MetaDataBinary { get; set; }
    }
}
