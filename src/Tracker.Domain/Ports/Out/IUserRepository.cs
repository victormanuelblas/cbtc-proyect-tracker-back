using Tracker.Domain.Entities;

namespace Tracker.Domain.Ports.Out
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int userId);
        Task<User?> GetUserByEmailAsync(string email);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task SaveUserAsync(User user);   
        Task UpdateUserAsync(User user);
        Task DeleteUserAsync(User user);
    }
}