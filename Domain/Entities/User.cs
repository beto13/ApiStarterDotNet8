﻿namespace Domain.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<Post> Posts { get; set; } = new();
    }
}
