

using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Products
{
    public class Product: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int NumReviews { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal PriceAfterDiscount { get; set; }
        public int CountInStock { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public List<Review> Reviews { get; set; } = new List<Review>();
        public double AverageRating { get; set; }

        public int GetNumberOfReviews()
        {
            return Reviews.Count;
        }

        public double GetAvgRating()
        {
            return Reviews.Count == 0 ? 0: Reviews.DefaultIfEmpty().Average(s => (int)s.Rating);
        }
    }
}
