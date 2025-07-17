using System;

namespace YuvamHazir.Domain.Entities
{
    public class ForumComment
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string Comment { get; set; }
        public DateTime CommentedAt { get; set; }
        public ForumPost Post { get; set; }
        public User User { get; set; }
    }
}