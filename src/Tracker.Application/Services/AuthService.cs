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


namespace Tracker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AuthService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _unitOfWork.UsersRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new UnauthorizedAccessException("Invalid email or password.");
            }
            
            if (!user.IsActive)
            {
                throw new BusinessRuleException("Active user rule","User account is not active");
            }
            
            if (!BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash))
            {
                throw new BusinessRuleException("Invalidid credentials","Email or password are incorrect.");
            }
            
            var token = GenerateJwtToken(user);

            var authResponse = new AuthResponseDto
            {
                UserId = user.UserId,
                Email = user.Email,
                Name = $"{user.Name}",
                Role = user.Role.ToString(),
                Token = token
            };

            return authResponse;
        }

        public string GenerateJwtToken(User user)
        {
            var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? throw new InvalidOperationException("JWT_SECRET environment variable is not set.");
            var jwtIssuer = Environment.GetEnvironmentVariable("JWT_ISSUER") ?? throw new InvalidOperationException("JWT_ISSUER environment variable is not set.");
            var jwtAudience = Environment.GetEnvironmentVariable("JWT_AUDIENCE") ??  throw new InvalidOperationException("JWT_AUDIENCE environment variable is not set.");

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSecret));

            var credencials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            
            var claims = new []
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