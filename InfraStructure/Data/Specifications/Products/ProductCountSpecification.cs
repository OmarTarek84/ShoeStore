
using Core.Entities.Products;
using Core.Helpers;

namespace InfraStructure.Data.Specifications.Products
{
    public class ProductCountSpecification: BaseSpecification<Product>
    {
        public ProductCountSpecification(ProductSpecParams productparams) : base(x =>
            (string.IsNullOrEmpty(productparams.ProductName) || x.Name.ToLower().Contains(productparams.ProductName.ToLower())) &&
            (!productparams.FromPrice.HasValue || x.PriceAfterDiscount >= productparams.FromPrice) &&
            (!productparams.ToPrice.HasValue || x.PriceAfterDiscount <= productparams.ToPrice) &&
            (!productparams.BrandId.HasValue || x.BrandId == productparams.BrandId)
        )
        { }
    }
}
