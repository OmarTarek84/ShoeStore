using Core.Dtos;
using Core.Entities.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IProductRepository: IBaseRepository<Product, ProductOutDto>
    {
        Task<Product> AddProduct(ProductInDto product);
        Task<ReviewOutDto> AddReview(ReviewInDto reviewInDto);
        Task<ReviewOutDto> GetUserReviewOfProduct(int productId);
    }
}
