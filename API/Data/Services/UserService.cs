using API.Data.Dtos;
using API.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace API.Data.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Response<UserDto>> CreateUserAsync(CreateUserDto user)
        {
            var appUser = new AppUser
            {
                PasswordHash = user.Password,
                Email = user.Email,
                NickName = user.NickName,
                UserName = user.Email
            };
            var result = await _userManager.CreateAsync(appUser, user.Password);
            return new Response<UserDto>
            {
                Data = MapUserDto(appUser)
            };
        }

        public async Task<Response<bool>> UpdateUserAsync(UpdateUserDto dto, int userId)
        {
            var appUser = await _userManager.FindByIdAsync(userId.ToString());

            if (appUser == null)
            {
                return new Response<bool>()
                {
                    Error = "User not found"
                };
            }

            if (dto.CurrentPassword != null && dto.Password != null)
            {
                var res = await _userManager.ChangePasswordAsync(appUser, dto.CurrentPassword, dto.Password);

                if (!res.Succeeded)
                {
                    return new Response<bool>()
                    {
                        Error = res.Errors.Select(e => e.Description).ToArray()
                    };
                }
            }

            if (dto.NickName != null)
            {
                appUser.NickName = dto.NickName;
            }

            var result = await _userManager.UpdateAsync(appUser);

            if (!result.Succeeded)
            {
                return new Response<bool>
                {
                    Error = result.Errors.Select(e => e.Description).ToArray()
                };
            }

            return new Response<bool>
            {
                Data = true
            };
        }

        public async Task<Response<UserDto>> DeleteUserAsync(int userId)
        {
            var appUser = await _userManager.FindByIdAsync(userId.ToString());
            if (appUser == null)
            {
                return new Response<UserDto>()
                {
                    Error = "User not found"
                };
            }

            var result = await _userManager.DeleteAsync(appUser);

            if (!result.Succeeded)
            {
                return new Response<UserDto>
                {
                    Error = result.Errors.Select(e => e.Description).ToArray()
                };
            }

            return new Response<UserDto>
            {
                Data = MapUserDto(appUser)
            };
        }

        public async Task<Response<UserDto>> GetUserByIdAsync(int userId)
        {
            var appUser = await _userManager.FindByIdAsync(userId.ToString());

            if (appUser == null)
            {
                return new Response<UserDto>()
                {
                    Error = "User not found"
                };
            }

            return new Response<UserDto>
            {
                Data = MapUserDto(appUser)
            };
        }

        private static UserDto MapUserDto(AppUser appUser)
        {
            return new UserDto
            {
                Id = appUser.Id,
                NickName = appUser.NickName,
                Email = appUser.Email,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}