using Core.Dtos;
using Core.Entities.Identity;
using Core.Interfaces;
using InfraStructure.Errors;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace API.Controllers.Identity
{
    public class AuthController: BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _uow;

        public AuthController(UserManager<AppUser> userManager, ITokenService tokenService, IUnitOfWork uow)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _uow = uow;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthOutDto>> Login(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user is null) return NotFound(new ApiException(400, "Email or password is incorrect"));

            if (!(await _userManager.CheckPasswordAsync(user, loginDto.Password)))
                return NotFound(new ApiException(400, "Email or password is incorrect"));

            var token = await _tokenService.CreateToken(user);
            return new AuthOutDto
            {
                Email = user.Email,
                Token = token
            };
        }

        [HttpPost("Register")]
        public async Task<ActionResult<AuthOutDto>> Register(RegisterInDto registerDto)
        {
            var user = await _userManager.FindByEmailAsync(registerDto.Email);
            if (user is not null) return BadRequest(new ApiException(400, "User with this email already exists"));

            var newUser = new AppUser
            {
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                JoinedAt = DateTime.Now,
                UserName = registerDto.UserName
            };

            var isUserRegistered = await _userManager.CreateAsync(newUser, registerDto.Password);

            if (!isUserRegistered.Succeeded) return BadRequest(new ApiException(500, "Unknown Error Occurred"));
            await _userManager.AddToRoleAsync(newUser, "User");

            var token = await _tokenService.CreateToken(newUser);
            return new AuthOutDto
            {
                Email = newUser.Email,
                Token = token
            };
        }

        [HttpPost("address")]
        [Authorize]
        public async Task<ActionResult<AddressOutDto>> InsertOrUpdateAddress(AddressInDto addressInDto)
        {
            Address address = await _uow.UserRepository.InsertOrUpdateAddress(addressInDto);
            if (address == null) return BadRequest(new ApiException(500, "Error in creating address"));
            return Ok(_uow.UserRepository.MapToDto(address));
        }

        [HttpGet("address")]
        [Authorize]
        public async Task<ActionResult<AddressOutDto>> GetAddress()
        {
            return Ok(await _uow.UserRepository.GetAddress());
        }

        [HttpGet("user")]
        [Authorize]
        public async Task<ActionResult<UserOutDto>> GetUser()
        {
            return Ok(await _uow.UserRepository.GetUser());
        }

        [HttpGet("email-exists")]
        public async Task<ActionResult<bool>> EmailExists([FromQuery] string email)
        {
            var usermail = await _userManager.FindByEmailAsync(email);
            return usermail is not null;
        }

        [HttpPatch("reset-password")]
        [Authorize]
        public async Task<ActionResult<bool>> ResetPassword(ResetPasswordDto resetPasswordDto)
        {
            var usermail = User.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(usermail);
            if (user is null) return BadRequest(new ApiException(500, "User not found"));

            var changedPassword = await _userManager.ChangePasswordAsync(user, resetPasswordDto.CurrentPassword, resetPasswordDto.NewPassword);
            if (changedPassword.Succeeded)
                return true;
            return BadRequest(new ApiException(500, changedPassword.Errors.FirstOrDefault()?.Description ?? "Error in resetting password"));
        }
    }
}
