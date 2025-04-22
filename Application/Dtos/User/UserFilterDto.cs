namespace Application.Dtos.User
{
    public class UserFilterDto
    {
        public string? Name { get; set; }
        public string? Email { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
