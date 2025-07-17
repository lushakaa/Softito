using System;
namespace YuvamHazir.API.DTOs
{
	public class BlogDto
	{
        public class BlogListDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string AuthorFullName { get; set; }
            public DateTime PublishedAt { get; set; }
        }

        public class BlogDetailDto
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string AuthorFullName { get; set; }
            public DateTime PublishedAt { get; set; }
            public List<BlogCommentDto> Comments { get; set; }
        }

        public class BlogCommentDto
        {
            public int Id { get; set; }
            public string Comment { get; set; }
            public string UserFullName { get; set; }
            public DateTime CreatedAt { get; set; }
        }

    }
}

