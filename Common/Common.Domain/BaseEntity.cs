namespace Common.Domain
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime CreationDate { get; }

        public BaseEntity()
        {
            CreationDate = new DateTime();
        }
    }
}