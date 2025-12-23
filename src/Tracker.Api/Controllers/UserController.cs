using Tracker.Application.DTOs.User;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;

namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }
        
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto createUserDto)
        {
            _logger.LogInformation(
                "POST /api/users requested. Email: {Email}",
                createUserDto.Email);

            try
            {
                var userDto = await _userService.CreateUserAsync(createUserDto);
                _logger.LogInformation(
                        "User {UserId} created successfully",
                        userDto.UserId);

                return CreatedAtAction(nameof(GetUserById), new { userId = userDto.UserId }, userDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Create user failed - {Entity} not found. Email: {Email}",
                    ex.EntityName,
                    createUserDto.Email);

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error creating user. Email: {Email}",
                    createUserDto.Email);

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserById(int userId)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/users/{UserId} requested by user {RequesterId}",
                userId,
                requesterId ?? "anonymous");

            try
            {
                var userDto = await _userService.GetUserByIdAsync(userId);
                _logger.LogDebug(
                        "User {UserId} retrieved successfully by user {RequesterId}",
                        userId,
                        requesterId ?? "anonymous");
                return Ok(userDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "User {UserId} not found. Requested by user {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving user {UserId}. Requester: {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserDto updateUserDto)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "PUT /api/users/{UserId} requested by user {RequesterId}",
                userId,
                requesterId ?? "anonymous");

            try
            {
                var userDto = await _userService.UpdateUserAsync(userId, updateUserDto);
                _logger.LogInformation(
                        "User {UserId} updated successfully by user {RequesterId}",
                        userId,
                        requesterId ?? "anonymous");
                return Ok(userDto);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Update failed - User {UserId} not found. Requester: {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error updating user {UserId}. Requester: {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "DELETE /api/users/{UserId} requested by user {RequesterId}",
                userId,
                requesterId ?? "anonymous");

            try
            {
                await _userService.DeleteUserAsync(userId);
                _logger.LogWarning(
                       "User {UserId} deleted by user {RequesterId}",
                       userId,
                       requesterId ?? "anonymous");
                return Ok(new
                {
                    message = "User deleted successfully"
                });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Delete failed - User {UserId} not found. Requester: {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error deleting user {UserId}. Requester: {RequesterId}",
                    userId,
                    requesterId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var requesterId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _logger.LogInformation(
                "GET /api/users requested by user {RequesterId}",
                requesterId ?? "anonymous");

            try
            {
                var users = await _userService.GetAllUsersAsync();
                _logger.LogInformation(
                        "Retrieved {UserCount} users for user {RequesterId}",
                        users.Count(),
                        requesterId ?? "anonymous");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError(
                    ex,
                    "Error retrieving all users. Requester: {RequesterId}",
                    requesterId ?? "anonymous");

                return StatusCode(500, new { message = "Internal server error" });
            }
        }
    }
}