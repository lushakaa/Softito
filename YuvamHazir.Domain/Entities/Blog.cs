using System;
using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Blog
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageUrl { get; set; }
        public DateTime PublishedAt { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
        public ICollection<BlogComment> BlogComments { get; set; }
    }
}