namespace Application.Dtos.User
{ 
    public record UserCreateDto
    {
        public string Name { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
    }
}
