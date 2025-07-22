using System;
// YuvamHazir.API/DTOs/PetCreateDto.cs
namespace YuvamHazir.API.DTOs
{
    public class PetCreateDto
    {
        public string Name { get; set; }
        public string Species { get; set; }
        public string Breed { get; set; }
        public string Gender { get; set; }
        public string ImageUrl { get; set; }
        public int Age { get; set; }
        public string Description { get; set; }
        public int OwnerId { get; set; }
    }
}


