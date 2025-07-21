using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Pet
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Species { get; set; }
    public string Breed { get; set; }
    public string Gender { get; set; }
    public string ImageUrl { get; set; }
    public int Age { get; set; }
    public string Description { get; set; }

    // BURAYI EKLE!
    public int OwnerId { get; set; }

    // Navigation
    public User Owner { get; set; }

    public ICollection<Adoption> Adoptions { get; set; }
}

}