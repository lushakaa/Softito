using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.API.DTOs;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;
using static YuvamHazir.API.DTOs.CategoryDto;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public CategoryController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Kategorileri listele
        [HttpGet]
        public async Task<ActionResult<List<CategoryListDto>>> GetCategories()
        {
            var cats = await _context.Categories.ToListAsync();
            var dtos = cats.Select(c => new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return Ok(dtos);
        }

        // Tek kategori getir
        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryListDto>> GetCategory(int id)
        {
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();

            var dto = new CategoryListDto
            {
                Id = c.Id,
                Name = c.Name
            };
            return Ok(dto);
        }

        // Yeni kategori ekle
        [HttpPost]
        public async Task<ActionResult> AddCategory(CategoryListDto dto)
        {
            var cat = new Category { Name = dto.Name };
            _context.Categories.Add(cat);
            await _context.SaveChangesAsync();
            return Ok(new { cat.Id });
        }

        // Kategori güncelle
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory(int id, CategoryListDto dto)
        {
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();

            c.Name = dto.Name;
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Kategori sil
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory(int id)
        {
            var c = await _context.Categories.FindAsync(id);
            if (c == null) return NotFound();

            _context.Categories.Remove(c);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
