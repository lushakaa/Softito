using Microsoft.AspNetCore.Mvc;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public PetController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // GET: api/pet
        [HttpGet]
        public IActionResult GetAllPets()
        {
            var pets = _context.Pets.ToList();
            return Ok(pets);
        }

        // POST: api/pet
        [HttpPost]
        public IActionResult AddPet([FromBody] Pet pet)
        {
            _context.Pets.Add(pet);
            _context.SaveChanges();
            return Ok(pet);
        }
    }
}
