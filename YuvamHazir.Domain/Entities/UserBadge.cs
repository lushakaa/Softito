using System;

namespace YuvamHazir.Domain.Entities
{
    public class UserBadge
    {
        public int UserId { get; set; }
        public int BadgeId { get; set; }
        public DateTime EarnedAt { get; set; }
        public User User { get; set; }
        public Badge Badge { get; set; }
    }
}