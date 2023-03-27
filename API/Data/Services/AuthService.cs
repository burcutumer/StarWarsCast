using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using API.Data.Entities;
using API.Data.Dtos;

namespace API.Data.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly string _secretKey;

        public AuthService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IConfiguration config)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _secretKey = config["Jwt:Secret"];// appsettings.Development icinde yarattik  "Jwt": { "Secret":
        }

        public async Task<Response<LoginResponseDto>> CheckUserCredentials(LoginRequestDto requestDto)
        {
            var user = await _userManager.FindByEmailAsync(requestDto.Email);
            if (user == null)
            {
                return new Response<LoginResponseDto>
                {
                    Error = "User is not found"
                };
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, requestDto.Password, false);

            if (!result.Succeeded)
            {
                return new Response<LoginResponseDto>
                {
                    Error = "User is not found"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);
            var claims = new List<Claim>{
                new Claim(ClaimTypes.Name, requestDto.Email)
            };
            // foreach(var r in roles)
            // {
            //     claims.Add(new Claim(ClaimTypes.Role, r));
            // }
            claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));

            // email ve sifre dogru artik token uretebilirim
            var token = GenerateJwtToken(_secretKey, 10, claims);

            return new Response<LoginResponseDto>
            {
                Data = new LoginResponseDto
                {
                    JwtToken = token
                }
            };
        }

        private static string GenerateJwtToken(string secretKey, int expireMinutes, List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(expireMinutes),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}