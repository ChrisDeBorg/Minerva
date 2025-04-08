namespace Minerva.DataModels
{
    public record BaseRecord
    {
        public int Id { get; init; }
    }
    public class BaseEntity
    {
        public int Id { get; set; }
    }
}
