

using Core.Dtos;
using Core.Entities.Identity;

namespace Core.Interfaces
{
    public interface IUserRepository
    {
        Task<AppUser> GetUserByIdAsync(string userId);
        Task<Address> InsertOrUpdateAddress(AddressInDto addressInDto);
        AddressOutDto MapToDto(Address address);
        Task<AddressOutDto> GetAddress();
        Task<UserOutDto> GetUser();
    }
}
