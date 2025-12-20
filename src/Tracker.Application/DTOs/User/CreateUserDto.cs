namespace Tracker.Application.DTOs.User
{
    public class CreateUserDto
    {
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Role { get; set; }
    }
}