using System;

namespace YuvamHazir.Domain.Entities
{
    public class PatiPoint
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int Points { get; set; }
        public string Reason { get; set; }
        public DateTime CreatedAt { get; set; }
        public User User { get; set; }
    }
}
