namespace Application.Dtos.Posts
{
    public class PostsCreateDto
    {
        public Guid UserId { get; set; }
        public string Title { get; set; } = string.Empty;
    }
}
