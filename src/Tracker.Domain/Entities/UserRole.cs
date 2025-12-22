using System.ComponentModel;

namespace Tracker.Domain.Entities;

public enum UserRole
{
    [Description("User")]
    User = 1,
    [Description("Admin")]
    Admin = 2,
    [Description("SuperAdmin")]
    SuperAdmin = 3
}
