namespace Application.Dtos.Comments
{
    public class CommentCreateDto
    {
        public string Content { get; set; } = string.Empty;
        public Guid PostId { get; set; }
    }
}
