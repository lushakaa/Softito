using System;

namespace YuvamHazir.Domain.Entities
{
    public class BlogComment
    {
        public int Id { get; set; }
        public int BlogId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public Blog Blog { get; set; }
        public User User { get; set; }
    }
}