using System.Collections.Generic;

namespace YuvamHazir.API.DTOs
{
    public class CartItemDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public bool IsSavedForLater { get; set; }
    }

    public class CartDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CouponCode { get; set; }
        public List<CartItemDto> Items { get; set; }
    }

    public class QuantityDto
    {
        public int Quantity { get; set; }
    }

}
