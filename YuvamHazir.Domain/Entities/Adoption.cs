using System;

namespace YuvamHazir.Domain.Entities
{
    public class Adoption
    {
        public int Id { get; set; }
        public int PetId { get; set; }
        public int OwnerId { get; set; }
        public int? NewOwnerId { get; set; }
        public bool IsAdopted { get; set; }
        public DateTime CreatedAt { get; set; }
        public Pet Pet { get; set; }
        public User Owner { get; set; }
        public User NewOwner { get; set; }
    }
}