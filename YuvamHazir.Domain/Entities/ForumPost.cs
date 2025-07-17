using System;
using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class ForumPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime PostedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public ICollection<ForumComment> ForumComments { get; set; }
    }
}
