using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.API.DTOs;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductRatingController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public ProductRatingController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Ürün için tüm puanları getir
        [HttpGet("product/{productId}")]
        public async Task<ActionResult<IEnumerable<ProductRatingDto>>> GetRatingsForProduct(int productId)
        {
            var ratings = await _context.Set<ProductRating>()
                .Where(r => r.ProductId == productId)
                .Select(r => new ProductRatingDto
                {
                    Id = r.Id,
                    UserId = r.UserId,
                    ProductId = r.ProductId,
                    Rating = r.Rating,
                    Comment = r.Comment,
                    CreatedAt = r.CreatedAt
                }).ToListAsync();

            return Ok(ratings);
        }

        // Yeni puan ekle
        [HttpPost]
        public async Task<ActionResult> AddRating(ProductRatingCreateDto dto)
        {
            var rating = new ProductRating
            {
                UserId = dto.UserId,
                ProductId = dto.ProductId,
                Rating = dto.Rating,
                Comment = dto.Comment,
                CreatedAt = DateTime.Now
            };
            _context.Add(rating);
            await _context.SaveChangesAsync();
            return Ok(rating.Id);
        }

        // (Ekstra) Puanı sil (gerekirse)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRating(int id)
        {
            var rating = await _context.Set<ProductRating>().FindAsync(id);
            if (rating == null)
                return NotFound();
            _context.Remove(rating);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}