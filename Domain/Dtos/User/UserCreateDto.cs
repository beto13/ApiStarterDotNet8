namespace Domain.Dtos.User
{
    public record UserCreateDto
    {
        public string Name { get; init; }
        public string Email { get; init; }
    }
}
