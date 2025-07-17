using System;
namespace YuvamHazir.API.DTOs
{
	public class OrderDto
	{
        public class OrderListDto
        {
            public int Id { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
        }

        public class OrderDetailDto
        {
            public int Id { get; set; }
            public DateTime OrderDate { get; set; }
            public decimal TotalPrice { get; set; }
            public List<OrderProductDto> Products { get; set; }
        }

        public class OrderProductDto
        {
            public int ProductId { get; set; }
            public string ProductName { get; set; }
            public int Quantity { get; set; }
            public decimal UnitPrice { get; set; }
        }

    }
}

