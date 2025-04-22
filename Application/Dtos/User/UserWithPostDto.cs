using Application.Dtos.Posts;

namespace Application.Dtos.User
{
    public class UserWithPostsDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public List<PostDto>? Posts { get; set; }
    }
}

