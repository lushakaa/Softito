﻿using System;
namespace YuvamHazir.API.DTOs
{
	public class ProductDto
	{
        // Ürün listeleme
        public class ProductListDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public string CategoryName { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }

            public double? AverageRating { get; set; }
        }


        // Ürün detay
        public class ProductDetailDto
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public int CategoryId { get; set; }
            public string CategoryName { get; set; }
            public double? AverageRating { get; set; } // YENİ!
            public List<ProductRatingDto> Ratings { get; set; } // İstersen son 3 yorumu da gösterebilirsin
        }

        // Ürün ekleme/güncelleme
        public class ProductCreateUpdateDto
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public string ImageUrl { get; set; }
            public decimal Price { get; set; }
            public int Stock { get; set; }
            public int CategoryId { get; set; }
        }

    }
}

