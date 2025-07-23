using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.Domain.Entities;
using YuvamHazir.Infrastructure.Context;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FavoritesController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public FavoritesController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // GET: api/favorites?userId=2
        [HttpGet]
        public async Task<IActionResult> GetUserFavorites([FromQuery] int userId)
        {
            var favorites = await _context.Favorites
                .Where(f => f.UserId == userId)
                .Select(f => f.ProductId)
                .ToListAsync();

            return Ok(favorites);
        }

        // POST: api/favorites/5  (productId)
        [HttpPost("{productId}")]
        public async Task<IActionResult> AddFavorite(int productId, [FromQuery] int userId)
        {
            if (await _context.Favorites.AnyAsync(f => f.UserId == userId && f.ProductId == productId))
                return BadRequest("Zaten favoride.");

            var fav = new Favorite
            {
                UserId = userId,
                ProductId = productId
            };
            _context.Favorites.Add(fav);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/favorites/5?userId=2
        [HttpDelete("{productId}")]
        public async Task<IActionResult> RemoveFavorite(int productId, [FromQuery] int userId)
        {
            var fav = await _context.Favorites.FirstOrDefaultAsync(f => f.UserId == userId && f.ProductId == productId);
            if (fav == null) return NotFound();

            _context.Favorites.Remove(fav);
            await _context.SaveChangesAsync();

            return Ok();
        }
    }
}
