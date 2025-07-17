using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using YuvamHazir.Infrastructure.Context;
using YuvamHazir.Domain.Entities;
using YuvamHazir.API.DTOs;
using static YuvamHazir.API.DTOs.OrderDto; // Nested class'lar için!

namespace YuvamHazir.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly YuvamHazirDbContext _context;

        public OrderController(YuvamHazirDbContext context)
        {
            _context = context;
        }

        // Siparişleri listele
        [HttpGet]
        public async Task<ActionResult<List<OrderListDto>>> GetOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            var dtoList = orders.Select(o => new OrderListDto
            {
                Id = o.Id,
                OrderDate = o.OrderDate,
                TotalPrice = o.TotalPrice
            }).ToList();

            return Ok(dtoList);
        }

        // Sipariş detay
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderDetailDto>> GetOrder(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            var dto = new OrderDetailDto
            {
                Id = order.Id,
                OrderDate = order.OrderDate,
                TotalPrice = order.TotalPrice,
                Products = order.OrderDetails.Select(od => new OrderProductDto
                {
                    ProductId = od.ProductId,
                    ProductName = od.Product.Name,
                    Quantity = od.Quantity,
                    UnitPrice = od.UnitPrice
                }).ToList()
            };

            return Ok(dto);
        }

        // Yeni sipariş ekle (örnek, ek isteklerine göre şekillendirilebilir)
        [HttpPost]
        public async Task<ActionResult> CreateOrder([FromBody] OrderDetailDto dto)
        {
            var order = new Order
            {
                OrderDate = dto.OrderDate,
                TotalPrice = dto.TotalPrice,
                // UserId ve AddressId gibi diğer alanları da eklemen gerekebilir!
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // OrderDetail kayıtlarını ekle
            if (dto.Products != null)
            {
                foreach (var prod in dto.Products)
                {
                    var orderDetail = new OrderDetail
                    {
                        OrderId = order.Id,
                        ProductId = prod.ProductId,
                        Quantity = prod.Quantity,
                        UnitPrice = prod.UnitPrice
                    };
                    _context.OrderDetails.Add(orderDetail);
                }
                await _context.SaveChangesAsync();
            }

            return Ok(new { order.Id });
        }

        // Sipariş sil
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.Include(o => o.OrderDetails)
                .FirstOrDefaultAsync(o => o.Id == id);

            if (order == null)
                return NotFound();

            _context.OrderDetails.RemoveRange(order.OrderDetails);
            _context.Orders.Remove(order);

            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
