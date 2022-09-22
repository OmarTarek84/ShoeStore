

namespace Core.Interfaces
{
    public interface IUnitOfWork
    {
        IProductRepository ProductRepository { get; }
        IUserRepository UserRepository { get; }
        ICartRepository CartRepository { get; }
        IBrandRepository BrandRepository { get; }
        IOrderRepository OrderRepository { get; }
        Task<bool> Complete();
        bool HasChanges();
    }
}
