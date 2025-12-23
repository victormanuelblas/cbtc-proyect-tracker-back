using System.ComponentModel.DataAnnotations;

namespace Tracker.Application.DTOs.User
{
    public class UpdateUserDto
    {
        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = null!;
        [Required]
        [EmailAddress]
        public string Email { get; set; }  = null!;
        public bool IsActive { get; set; }
        [Required]
        [StringLength(20)]
        public string Role { get; set; } = null!;
    }
}