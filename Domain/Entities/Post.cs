namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;
        public List<Comment> Comments { get; set; } = new();
    }
}
