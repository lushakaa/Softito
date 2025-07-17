using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Badge
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string IconUrl { get; set; }
        public string Description { get; set; }
        public ICollection<UserBadge> UserBadges { get; set; }
    }
}