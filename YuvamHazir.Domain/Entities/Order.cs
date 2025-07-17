using System;
using System.Collections.Generic;

namespace YuvamHazir.Domain.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AddressId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalPrice { get; set; }
        public User User { get; set; }
        public Address Address { get; set; }
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }
}