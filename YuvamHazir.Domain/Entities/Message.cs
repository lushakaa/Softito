using System;

namespace YuvamHazir.Domain.Entities
{
    public class Message
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public string Content { get; set; }
        public DateTime SentAt { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
