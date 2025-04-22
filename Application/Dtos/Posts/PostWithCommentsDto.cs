using Application.Dtos.Comments;

namespace Application.Dtos.Posts
{
    public class PostWithCommentsDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public IEnumerable<CommentDto>? CommentsDto { get; set; }
    }
}
