
using AutoMapper;
using Core.Interfaces;
using InfraStructure.Data.Repositories.Carts;
using InfraStructure.Data.Repositories.Identity;
using InfraStructure.Data.Repositories.Orders;
using InfraStructure.Data.Repositories.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace InfraStructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IImageService _imageService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _config;

        public UnitOfWork(StoreContext context, IMapper mapper, IImageService imageService, IHttpContextAccessor contextAccessor, IConfiguration config)
        {
            _context = context;
            _mapper = mapper;
            _imageService = imageService;
            _contextAccessor = contextAccessor;
            _config = config;
        }
        public IProductRepository ProductRepository => new ProductRepository(_context, _mapper, _imageService, _contextAccessor);

        public IUserRepository UserRepository => new UserRepository(_context, _mapper, _contextAccessor);

        public IBrandRepository BrandRepository => new BrandRepository(_context, _mapper);

        public ICartRepository CartRepository => new CartRepository(_context, _mapper, _contextAccessor);

        public IOrderRepository OrderRepository => new OrderRepository(_context, _mapper, _contextAccessor, _config);

        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
