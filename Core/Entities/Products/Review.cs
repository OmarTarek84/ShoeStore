

using Core.Entities.Identity;
using Core.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Products
{
    public class Review
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public Rating Rating { get; set; }
        public string? Comment { get; set; }
        public string UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public AppUser User { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
