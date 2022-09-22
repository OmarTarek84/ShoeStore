using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Dtos
{
    public class ProductOutDto : OutDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int NumberOfReviews { get; set; }
        public decimal OriginalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; }
        public string BrandName { get; set; }
        public double AvgRating { get; set; }
        public int CountInStock { get; set; }
        public List<ReviewOutDto> Reviews { get; set; } = new List<ReviewOutDto>();
    }

    public class ProductInDto
    {
        [Required]
        public IFormFile productImageFile { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; } = 0;
        [Required]
        public int CountInStock { get; set; }
        [Required]
        public int BrandId { get; set; }
    }

    public class ProductUpdateDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        public decimal PriceAfterDiscount { get; set; } = 0;
        [Required]
        public int CountInStock { get; set; }
        [Required]
        public int BrandId { get; set; }
    }
}