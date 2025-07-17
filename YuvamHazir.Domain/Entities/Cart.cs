using System;
using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public string? CouponCode { get; set; }
        public User User { get; set; }
        public ICollection<CartItem> CartItems { get; set; }
    }
}