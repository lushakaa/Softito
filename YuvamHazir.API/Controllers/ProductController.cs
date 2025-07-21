using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.Domain.Entities;
using YuvamHazir.API.DTOs;
using static YuvamHazir.API.DTOs.ProductDto;

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public ProductController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Tüm ürünleri listele
        [HttpGet]
        public async Task<ActionResult<List<ProductListDto>>> GetProducts()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .ToListAsync();

            // Tüm rating'leri grup halinde al
            var ratings = await _context.Set<ProductRating>()
                .GroupBy(r => r.ProductId)
                .Select(g => new
                {
                    ProductId = g.Key,
                    Average = g.Average(r => r.Rating)
                }).ToListAsync();

            var dtoList = products.Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                Price = p.Price,
                Stock = p.Stock,
                CategoryName = p.Category?.Name,
                Description = p.Description,
                AverageRating = ratings.FirstOrDefault(r => r.ProductId == p.Id)?.Average
            }).ToList();

            return Ok(dtoList);
        }


        // Ürün detay
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDetailDto>> GetProduct(int id)
        {
            var product = await _context.Products.Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            // Ürün için ortalama rating hesapla
            var averageRating = await _context.Set<ProductRating>()
                .Where(r => r.ProductId == id)
                .AverageAsync(r => (double?)r.Rating);

            var dto = new ProductDetailDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageUrl = product.ImageUrl,
                Price = product.Price,
                Stock = product.Stock,
                CategoryId = product.CategoryId,
                CategoryName = product.Category?.Name,
                AverageRating = averageRating // Ortalama rating ekledik
            };

            return Ok(dto);
        }

        // Ürün ekle
        [HttpPost]
        public async Task<ActionResult> AddProduct(ProductCreateUpdateDto dto)
        {
            var product = new Product
            {
                Name = dto.Name,
                Description = dto.Description,
                ImageUrl = dto.ImageUrl,
                Price = dto.Price,
                Stock = dto.Stock,
                CategoryId = dto.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new { product.Id });
        }

        // Ürün güncelle
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct(int id, ProductCreateUpdateDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            product.Name = dto.Name;
            product.Description = dto.Description;
            product.ImageUrl = dto.ImageUrl;
            product.Price = dto.Price;
            product.Stock = dto.Stock;
            product.CategoryId = dto.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Ürün sil
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
