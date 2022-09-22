

namespace Core.Helpers
{
    public class ProductSpecParams: PaginationParams
    {
        public string? ProductName { get; set; }
        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }
        public string? Sort { get; set; }
        public int? BrandId { get; set; }
    }
}
