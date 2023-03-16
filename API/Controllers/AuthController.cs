using API.Data.Dtos;
using API.Data.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class AuthController: BaseApiController
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<ActionResult<Response<LoginResponseDto>>> CheckUserCredentials(LoginRequestDto requestDto)
        {
            var result = await _authService.CheckUserCredentials(requestDto);

            if (result.Data == null)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }
    }
}