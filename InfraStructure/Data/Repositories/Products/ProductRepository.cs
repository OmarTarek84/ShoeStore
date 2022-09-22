using AutoMapper;
using Core.Dtos;
using Core.Entities.Products;
using Core.Enums;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace InfraStructure.Data.Repositories.Products
{
    public class ProductRepository : BaseRepository<Product, ProductOutDto>, IProductRepository
    {
        private readonly StoreContext _context;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ProductRepository(StoreContext context, IMapper mapper, IImageService imageService, IHttpContextAccessor contextAccessor) : base(context, mapper)
        {
            _context = context;
            _imageService = imageService;
            _contextAccessor = contextAccessor;
        }

        public async Task<Product> AddProduct(ProductInDto productInDto)
        {
            var result = await _imageService.AddPhotoAsync(productInDto.productImageFile);
            if (result.Error != null) return null;

            var brand = await _context.Brands.FindAsync(productInDto.BrandId);
            if (brand == null) return null;

            var newProduct = new Product
            {
                AverageRating = 0,
                BrandId = productInDto.BrandId,
                CountInStock = productInDto.CountInStock,
                Description = productInDto.Description,
                Image = result.SecureUrl.AbsoluteUri,
                NumReviews = 0,
                Brand = brand,
                OriginalPrice = productInDto.OriginalPrice,
                PriceAfterDiscount = productInDto.PriceAfterDiscount != 0 ? productInDto.PriceAfterDiscount : productInDto.OriginalPrice,
                Name = productInDto.Name
            };

            return Add(newProduct);
        }

        public async Task<ReviewOutDto> GetUserReviewOfProduct(int productId)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userReview = await _context.Reviews.FindAsync(productId, userId);
            if (userReview == null) return null;
            return new ReviewOutDto
            {
                Rating = userReview.Rating,
                Comment = userReview.Comment,
                ProductId = productId,
                CreateDate = userReview.CreateDate,
            };
        }

        public async Task<ReviewOutDto> AddReview(ReviewInDto reviewInDto)
        {
            var rating = reviewInDto.Rating;

            const Rating RATING_MINIMUM = Rating.Poor;
            const Rating RATING_MAXIMUM = Rating.Excellent;

            if (rating < RATING_MINIMUM) { rating = RATING_MINIMUM; }
            if (rating > RATING_MAXIMUM) { rating = RATING_MAXIMUM; }


            var product = await _context.Products.FindAsync(reviewInDto.ProductId);
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (product == null || product.CreatedBy == userId) return null;

            var userReview = await _context.Reviews.FindAsync(product.Id, userId);
            if (userReview == null)
            {
                var review = new Review
                {
                    Comment = reviewInDto.Comment,
                    ProductId = reviewInDto.ProductId,
                    Rating = rating,
                    CreateDate = DateTime.Now,
                    UserId = userId
                };

                product.Reviews.Add(review);
                var saveChanges = await _context.SaveChangesAsync();
                if (saveChanges > 0)
                {
                    return new ReviewOutDto
                    {
                        Comment = review.Comment,
                        ProductId = review.ProductId,
                        Rating = review.Rating,
                        CreateDate = review.CreateDate,
                        UserEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value,
                        UserName = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value,
                    };
                }
            }
            else
            {
                userReview.Rating = rating;
                userReview.Comment = reviewInDto.Comment;
                userReview.CreateDate = DateTime.Now;
                var saveChanges = await _context.SaveChangesAsync();
                if (saveChanges > 0)
                {
                    return new ReviewOutDto
                    {
                        Comment = userReview.Comment,
                        ProductId = userReview.ProductId,
                        Rating = userReview.Rating,
                        CreateDate = userReview.CreateDate,
                        UserEmail = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.Email).Value,
                        UserName = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.GivenName).Value,
                    };
                }
            }
            return null;
        }
    }
}