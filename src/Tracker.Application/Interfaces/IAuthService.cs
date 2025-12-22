using Tracker.Application.DTOs.Auth;

namespace Tracker.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
    }
}