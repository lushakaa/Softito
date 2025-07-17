using System;

namespace YuvamHazir.Domain.Entities
{
    public class FosterRequest
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; } // Pending, Accepted, Rejected
        public DateTime RequestedAt { get; set; }
        public DateTime? AcceptedAt { get; set; }
        public User User { get; set; }
    }
}
