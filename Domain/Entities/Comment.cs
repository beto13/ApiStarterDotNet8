namespace Domain.Entities
{
    public class Comment : BaseEntity
    {
        public string Content { get; set; }
        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
