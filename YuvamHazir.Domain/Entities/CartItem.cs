using System;

namespace YuvamHazir.Domain.Entities
{
    public class CartItem
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public bool IsSavedForLater { get; set; } = false;
        public DateTime AddedAt { get; set; } = DateTime.Now;
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}