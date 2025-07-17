using System;

namespace YuvamHazir.Domain.Entities
{
    public class AdoptionContract
    {
        public int Id { get; set; }
        public int AdoptionId { get; set; }
        public string FileUrl { get; set; }
        public bool IsSignedByOwner { get; set; }
        public bool IsSignedByNewOwner { get; set; }
        public DateTime CreatedAt { get; set; }
        public Adoption Adoption { get; set; }
    }
}