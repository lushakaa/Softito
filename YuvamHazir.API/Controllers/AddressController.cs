using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.Domain.Entities;
using YuvamHazir.API.DTOs;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AddressController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public AddressController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Adresleri listele (Tüm adresleri veya userId'ye göre filtreleyebilirsin)
        [HttpGet]
        public async Task<ActionResult<List<AddressDto>>> GetAddresses([FromQuery] int? userId)
        {
            var query = _context.Addresses.AsQueryable();
            if (userId.HasValue)
                query = query.Where(a => a.UserId == userId.Value);

            var list = await query.ToListAsync();
            var dtos = list.Select(a => new AddressDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                UserId = a.UserId
            }).ToList();

            return Ok(dtos);
        }

        // Tek adres getir
        [HttpGet("{id}")]
        public async Task<ActionResult<AddressDto>> GetAddress(int id)
        {
            var a = await _context.Addresses.FindAsync(id);
            if (a == null) return NotFound();

            var dto = new AddressDto
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                UserId = a.UserId
            };
            return Ok(dto);
        }

        // Adres ekle
        [HttpPost]
        public async Task<ActionResult> AddAddress(AddressDto dto)
        {
            var address = new Address
            {
                Title = dto.Title,
                Description = dto.Description,
                UserId = dto.UserId
            };
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return Ok(new { address.Id });
        }

        // Adres güncelle
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddress(int id, AddressDto dto)
        {
            var a = await _context.Addresses.FindAsync(id);
            if (a == null) return NotFound();

            a.Title = dto.Title;
            a.Description = dto.Description;
            a.UserId = dto.UserId;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Adres sil
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddress(int id)
        {
            var a = await _context.Addresses.FindAsync(id);
            if (a == null) return NotFound();

            _context.Addresses.Remove(a);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
