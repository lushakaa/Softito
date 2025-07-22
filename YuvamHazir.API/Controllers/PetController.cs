using Microsoft.AspNetCore.Mvc;
using YuvamHazir.API.DTOs;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using static OpenAI.ObjectModels.StaticValues.AssistantsStatics.MessageStatics;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;
        private readonly OpenAiService _openAi;

        public PetController(YuvamHazirDbContext context, OpenAiService openAi)
        {
            _context = context;
            _openAi = openAi;
        }

        // GET: api/pet
        [HttpGet]
        public IActionResult GetAllPets()
        {
            var pets = _context.Pets
                .Include(p => p.Owner)
                .Select(p => new
                {
                    p.Id,
                    p.Name,
                    p.Species,
                    p.Breed,
                    p.Gender,
                    p.ImageUrl,
                    p.Age,
                    p.Description,
                    OwnerName = p.Owner.FullName
                })
                .ToList();

            return Ok(pets);
        }

        // POST: api/pet
        //aaaa
        [HttpPost]
    public async Task<IActionResult> AddPet([FromBody] PetCreateDto petDto)
    {
        if (!string.IsNullOrWhiteSpace(petDto.Description))
        {
            petDto.Description = await _openAi.EnhanceDescriptionAsync(petDto.Description);
        }

        var pet = new Pet
        {
            Name = petDto.Name,
            Species = petDto.Species,
            Breed = petDto.Breed,
            Gender = petDto.Gender,
            ImageUrl = petDto.ImageUrl,
            Age = petDto.Age,
            Description = petDto.Description,
            OwnerId = petDto.OwnerId
        };

        _context.Pets.Add(pet);
        await _context.SaveChangesAsync();

        // ✔️ Owner ile birlikte yeniden yükle
        var result = await _context.Pets
            .Include(p => p.Owner)
            .FirstOrDefaultAsync(p => p.Id == pet.Id);

        return Ok(new
        {
            result.Id,
            result.Name,
            result.Species,
            result.Breed,
            result.Gender,
            result.ImageUrl,
            result.Age,
            result.Description,
            result.OwnerId,
            OwnerName = result.Owner?.FullName
        });
    }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPetById(int id)
        {
            var pet = await _context.Pets
                .Include(p => p.Owner)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (pet == null)
                return NotFound();

            return Ok(new
            {
                pet.Id,
                pet.Name,
                pet.Species,
                pet.Breed,
                pet.Gender,
                pet.ImageUrl,
                pet.Age,
                pet.Description,
                Owner = new
                {
                    Id = pet.OwnerId,
                    FullName = pet.Owner?.FullName,
                    PhoneNumber = pet.Owner?.PhoneNumber
                }
            });
        }


    }
}
