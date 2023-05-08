namespace MyCookBook.Domain.Entities
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
