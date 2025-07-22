using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.Domain.Entities;
using YuvamHazir.API.DTOs;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public CartController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Kullanıcının sepetini getir
        [HttpGet("{userId}")]
        public async Task<ActionResult<CartDto>> GetCart(int userId)
        {
            var cart = await _context.Carts
                .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
                return Ok(new CartDto { UserId = userId, Items = new List<CartItemDto>() }); // Boş sepet döner

            var dto = new CartDto
            {
                Id = cart.Id,
                UserId = cart.UserId,
                CouponCode = cart.CouponCode,
                Items = cart.CartItems.Select(ci => new CartItemDto
                {
                    Id = ci.Id,
                    ProductId = ci.ProductId,
                    ProductName = ci.Product.Name,
                    ImageUrl = ci.Product.ImageUrl,
                    UnitPrice = ci.UnitPrice,
                    Quantity = ci.Quantity,
                    IsSavedForLater = ci.IsSavedForLater
                }).ToList()
            };
            return Ok(dto);
        }

        // Sepete ürün ekle
        [HttpPost("{userId}/add")]
        public async Task<ActionResult> AddToCart(int userId, [FromBody] CartItemDto dto)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    CreatedAt = DateTime.Now,
                    CartItems = new List<CartItem>()
                };
                _context.Carts.Add(cart);
            }

            var existingItem = cart.CartItems.FirstOrDefault(ci => ci.ProductId == dto.ProductId && !ci.IsSavedForLater);
            if (existingItem != null)
            {
                existingItem.Quantity += dto.Quantity;
            }
            else
            {
                var newItem = new CartItem
                {
                    ProductId = dto.ProductId,
                    Quantity = dto.Quantity,
                    UnitPrice = dto.UnitPrice,
                    IsSavedForLater = false,
                    AddedAt = DateTime.Now
                };
                cart.CartItems.Add(newItem);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        // Sepetten ürün çıkar
        [HttpDelete("{userId}/remove/{cartItemId}")]
        public async Task<ActionResult> RemoveFromCart(int userId, int cartItemId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return NotFound();

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item == null) return NotFound();

            _context.CartItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Sepette ürün miktarını güncelle
        [HttpPut("{userId}/update/{cartItemId}")]
        public async Task<ActionResult> UpdateCartItem(int userId, int cartItemId, [FromBody] QuantityDto dto)
        {
            if (dto.Quantity < 1)
                return BadRequest("Quantity must be at least 1.");

            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return NotFound();

            var item = cart.CartItems.FirstOrDefault(ci => ci.Id == cartItemId);
            if (item == null) return NotFound();

            item.Quantity = dto.Quantity;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // Sepeti tamamen boşalt
        [HttpDelete("{userId}/clear")]
        public async Task<ActionResult> ClearCart(int userId)
        {
            var cart = await _context.Carts.Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null) return NotFound();

            _context.CartItems.RemoveRange(cart.CartItems);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
