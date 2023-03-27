using API.Data.Dtos;
using API.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        private readonly IUserService _userInterface;
        public UserController(IUserService userInterface)
        {
            _userInterface = userInterface;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Response<UserDto>>> DeleteUser(int id)
        {
            var result = await _userInterface.DeleteUserAsync(id);

            if (result.Error != null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Response<UserDto>>> GetUser(int id)
        {
            var result = await _userInterface.GetUserByIdAsync(id);
            if (result.Data != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<Response<UserDto>>> CreateUser([FromBody] CreateUserDto user)
        {
            var result = await _userInterface.CreateUserAsync(user);

            if (result.Data != null)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Response<bool>>> UpdateUser([FromBody] UpdateUserDto dto, int id)
        {
            var result = await _userInterface.UpdateUserAsync(dto, id);

            if (result.Error != null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}