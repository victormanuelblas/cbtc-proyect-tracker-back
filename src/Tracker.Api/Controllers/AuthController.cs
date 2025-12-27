using Tracker.Application.DTOs.Auth;
using Tracker.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Tracker.Domain.Exceptions;
using Microsoft.Extensions.Logging;

namespace Tracker.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            _logger.LogInformation(
                "POST /api/auth/login requested for user: {Email}",
                loginDto.Email);
            try
            {
                var authResponse = await _authService.LoginAsync(loginDto);
                _logger.LogInformation(
                    "Login successful for user: {Email}. UserId: {UserId}",
                    loginDto.Email,
                    authResponse.UserId);
                return Ok(authResponse);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(
                    "Login failed - Unauthorized access for user: {Email}. Reason: {ErrorMessage}",
                    loginDto.Email,
                    ex.Message);
                return Unauthorized(new { message = ex.Message });
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(
                    "Login failed - User not found: {Email}. Entity: {EntityName}",
                    loginDto.Email,
                    ex.EntityName);

                return Unauthorized(new { message = "Invalid credentials" });
            }
            catch (DomainException ex)
            {
                _logger.LogWarning(
                    "Login failed - Domain validation error for user: {Email}. Error: {ErrorMessage}",
                    loginDto.Email,
                    ex.Message);

                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {

              _logger.LogError(
                  ex,
                  "Error during login process for user: {Email}. Exception Type: {ExceptionType}, Message: {ErrorMessage}",
                  loginDto.Email,
                  ex.GetType().Name,
                  ex.Message);
              return StatusCode(500, new { message = "Internal server error", detail = ex.Message });

            }
        }
    }
}
