using AutoMapper;
using Core.Dtos;
using Core.Entities.Identity;
using Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace InfraStructure.Data.Repositories.Identity
{
    public class UserRepository : IUserRepository
    {
        private readonly StoreContext _context;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserRepository(StoreContext context, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }

        public async Task<AppUser> GetUserByIdAsync(string userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user is not null) return user;

            return null;
        }

        public AddressOutDto MapToDto(Address address)
        {
            return _mapper.Map<AddressOutDto>(address);
        }

        public async Task<AddressOutDto> GetAddress()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var address = await _context.Addresses.FirstOrDefaultAsync(s => s.AppUserId == userId);
            if (address == null) return new AddressOutDto();
            return _mapper.Map<AddressOutDto>(address);
        }

        public async Task<UserOutDto> GetUser()
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            var appUser = await _context.Users.FindAsync(userId);
            var cartItemsCount = await _context.CartItems.CountAsync(c => c.UserId == userId);
            var address = await _context.Addresses.FirstOrDefaultAsync(a => a.AppUserId == userId);


            return new UserOutDto
            {
                Address = _mapper.Map<AddressOutDto>(address),
                CartItemsCount = cartItemsCount,
                Email = appUser.Email,
                FirstName = appUser.FirstName,
                JoinedAt = appUser.JoinedAt,
                LastName = appUser.LastName,
                UserName = appUser.UserName
            };
        }

        public async Task<Address> InsertOrUpdateAddress(AddressInDto addressInDto)
        {
            var userId = _contextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var userAddress = await _context.Addresses.FirstOrDefaultAsync(s => s.AppUserId == userId);

            if (userAddress is not null)
            {

                userAddress.City = addressInDto.City;
                userAddress.Country = addressInDto.Country;
                userAddress.FirstName = addressInDto.FirstName;
                userAddress.LastName = addressInDto.LastName;
                userAddress.Street = addressInDto.Street;
                userAddress.ZipCode = addressInDto.ZipCode;

                var ad = _context.Addresses.Update(userAddress);
                return ad.Entity;
            }

            var newaddress = new Address
            {
                AppUserId = userId,
                City = addressInDto.City,
                Country = addressInDto.Country,
                FirstName = addressInDto.FirstName,
                LastName = addressInDto.LastName,
                Street = addressInDto.Street,
                ZipCode = addressInDto.ZipCode
            };

            var isSuccess = _context.Addresses.AddAsync(newaddress).IsCompletedSuccessfully;
            if (isSuccess) return newaddress;
            return null;
        }
    }
}
