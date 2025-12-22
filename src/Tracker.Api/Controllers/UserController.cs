using Tracker.Application.DTOs.User;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;

namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            var userDto = await _userService.CreateUserAsync(createUserDto);
            return CreatedAtAction(nameof(GetUserById), new { userId = userDto.UserId }, userDto);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var userDto = await _userService.GetUserByIdAsync(userId);
            return Ok(userDto);
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var userDto = await _userService.UpdateUserAsync(userId, updateUserDto);
            return Ok(userDto);
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUserAsync(userId);
            return Ok(new
            {
                message = "User deleted successfully"
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
    }
}