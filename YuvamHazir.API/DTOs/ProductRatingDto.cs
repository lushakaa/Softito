using System;
using System.ComponentModel.DataAnnotations;

namespace YuvamHazir.API.DTOs
{
    public class ProductRatingCreateOrUpdateDto
    {
        public int ProductId { get; set; }

        [Range(1, 5, ErrorMessage = "Rating must be between 1 and 5.")]
        public int Rating { get; set; }

        [MaxLength(500)]
        public string? Comment { get; set; }
    }

    // Ürün detay sayfasında gösterilecek DTO
    public class ProductRatingResponseDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public string UserFullName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}

