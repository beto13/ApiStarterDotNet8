namespace Application.Dtos.Comments
{
    public class CommentDto
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public Guid PostId { get; set; }
    }
}
