using Core.Dtos;
using Core.Entities.Products;
using Core.Helpers;
using Core.Interfaces;
using InfraStructure.Data.Specifications.Products;
using InfraStructure.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Products
{
    public class ProductController: BaseApiController
    {
        private readonly IUnitOfWork _uow;

        public ProductController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductOutDto>>> GetProducts([FromQuery] ProductSpecParams productParams)
        {
            var prods = await _uow.ProductRepository.GetListAsync(
                new ProductSpecification(productParams),
                new ProductCountSpecification(productParams),
                new PaginationParams { PageNumber = productParams.PageNumber, PageSize = productParams.PageSize }
            );

            return Ok(prods);
        }

        [HttpGet("user-review")]
        [Authorize]
        public async Task<ReviewOutDto> GetUserReviewOfProduct(int productId)
        {
            return await _uow.ProductRepository.GetUserReviewOfProduct(productId);
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<ProductOutDto>> AddProduct([FromForm]ProductInDto productInDto)
        {
            var newProd = await _uow.ProductRepository.AddProduct(productInDto);
            if (newProd is null) BadRequest(new ApiException(500));
            if (await _uow.Complete()) return Ok(_uow.ProductRepository.MapToDto(newProd));
            return BadRequest(new ApiException(400));
        }

        [HttpGet("{productId}")]
        public async Task<ProductOutDto> GetProduct(int productId)
        {
            return await _uow.ProductRepository.GetByIdAsync(new ProductSpecification(productId));
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<BrandOutDto>> UpdateProduct(ProductUpdateDto productUpdateDto)
        {
            var updatedProduct = _uow.ProductRepository.Update(new Product
            {
                Id = productUpdateDto.Id,
                Name = productUpdateDto.Name,
                BrandId = productUpdateDto.BrandId,
                CountInStock = productUpdateDto.CountInStock,
                OriginalPrice = productUpdateDto.OriginalPrice,
                PriceAfterDiscount = productUpdateDto.PriceAfterDiscount,
                Description = productUpdateDto.Description
            }, new string[] {"Image", "NumReviews", "AverageRating" });
            if (updatedProduct is null) return BadRequest(new ApiException(400, "Item Not Found"));

            if (await _uow.Complete()) return Ok(await GetProduct(updatedProduct.Id));
            return BadRequest(new ApiException(400));
        }

        [HttpPost("add-review")]
        [Authorize]
        public async Task<ActionResult<ReviewOutDto>> AddReview(ReviewInDto reviewInDto)
        {
            var resultReview = await _uow.ProductRepository.AddReview(reviewInDto);
            if (resultReview == null) return Ok(new ApiException(400, "Unknown Error Occurred"));
            return Ok(resultReview);
        }
    }
}
