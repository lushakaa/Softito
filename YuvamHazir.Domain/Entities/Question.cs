using System;
using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Question
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public QuestionCategory Category { get; set; }
        public ICollection<Answer> Answers { get; set; }
    }
}