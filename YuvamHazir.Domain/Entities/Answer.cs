using System;

namespace YuvamHazir.Domain.Entities
{
    public class Answer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
        public DateTime AnsweredAt { get; set; }
        public Question Question { get; set; }
        public User User { get; set; }
    }
}