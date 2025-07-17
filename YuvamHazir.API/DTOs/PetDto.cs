using System;
namespace YuvamHazir.API.DTOs
{
	public class PetDto
	{
        public class PetListDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Species { get; set; }
            public string Breed { get; set; }
            public string Gender { get; set; }
            public string ImageUrl { get; set; }
            public int Age { get; set; }
        }

    }
}

