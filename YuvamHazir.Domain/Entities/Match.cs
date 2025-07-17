using System;

namespace YuvamHazir.Domain.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int PetId { get; set; }
        public double Score { get; set; }
        public DateTime MatchedAt { get; set; }
        public User User { get; set; }
        public Pet Pet { get; set; }
    }
}
