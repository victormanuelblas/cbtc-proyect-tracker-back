using AutoMapper;
using Tracker.Domain.Entities;
using Tracker.Domain.Ports.Out;
using Tracker.Domain.Exceptions;
using Tracker.Application.DTOs.User;
using Tracker.Application.DTOs.Auth;
using Tracker.Application.Interfaces;

using System.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Logging;

namespace Tracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<AuthService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            _logger.LogDebug("Login attempt for email: {UserEmail}", loginDto.Email);
            try
            {
                var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(loginDto.Email);
                if (user == null)
                {
                    _logger.LogWarning("Login failed - User not found with email: {UserEmail}", loginDto.Email);

                    throw new UnauthorizedAccessException("Invalid email or password.");
                }
                _logger.LogDebug("User {UserId} found, checking account status", user.UserId);


                if (!user.IsActive)
                {
                    _logger.LogWarning(
                           "Login failed - User {UserId} ({UserEmail}) account is not active",
                           user.UserId,
                           user.Email);
                    throw new BusinessRuleException("Active user rule", "User account is not active");
                }
                _logger.LogDebug("Verifying password for user {UserId}", user.UserId);


                if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
                {
                    _logger.LogWarning(
                            "Login failed - Invalid password for user {UserId} ({UserEmail})",
                            user.UserId,
                            user.Email);
                    throw new BusinessRuleException("Invalid credentials", "Email or password are incorrect.");
                }
                _logger.LogDebug("Generating JWT token for user {UserId}", user.UserId);


                var token = GenerateJwtToken(user);

                var authResponse = new AuthResponseDto
                {
                    UserId = user.UserId,
                    Email = user.Email,
                    Name = $"{user.Name}",
                    Role = user.Role.ToString(),
                    Token = token
                };
                _logger.LogInformation(
                        "Login successful for user {UserId} ({UserEmail}) with role {UserRole}",
                        user.UserId,
                        user.Email,
                        user.Role);

                return authResponse;
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogDebug("Unauthorized login attempt: {Message}", ex.Message);
                throw;
            }
            catch (BusinessRuleException){throw;}
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for email: {UserEmail}", loginDto.Email);
                throw;
            }
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET environment variable is not set.");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER environment variable is not set.");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ?? throw new InvalidOperationException("JWT_AUDIENCE environment variable is not set.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString()),
                new Claim("role", user.Role.ToString())
            };

            var token = new JwtSecurityToken(
                issuer: jwtIssuer,
                audience: jwtAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credencials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}