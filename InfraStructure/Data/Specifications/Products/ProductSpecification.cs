using Core.Entities.Products;
using Core.Helpers;

namespace InfraStructure.Data.Specifications.Products
{
    public class ProductSpecification: BaseSpecification<Product>
    {
        public ProductSpecification(ProductSpecParams productparams): base(x => 
            (string.IsNullOrEmpty(productparams.ProductName) || x.Name.ToLower().Contains(productparams.ProductName.ToLower())) &&
            (!productparams.FromPrice.HasValue || x.PriceAfterDiscount >= productparams.FromPrice) &&
            (!productparams.ToPrice.HasValue || x.PriceAfterDiscount <= productparams.ToPrice) &&
            (!productparams.BrandId.HasValue || x.BrandId == productparams.BrandId)
        )
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Reviews);

            ApplyPaging(
                (productparams.PageNumber - 1) * productparams.PageSize,
                productparams.PageSize
            );

            if (!string.IsNullOrEmpty(productparams.Sort))
            {
                switch (productparams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(x => x.PriceAfterDiscount);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(x => x.PriceAfterDiscount);
                        break;
                    default:
                        AddOrderBy(x => x.Name);
                        break;
                }
            }
        }

        public ProductSpecification(int id): base(x => x.Id == id)
        {
            AddInclude(x => x.Brand);
            AddInclude(x => x.Reviews);
        }
    }
}
