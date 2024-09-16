using API;
using API.Models.Domain.Extra;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class ApplicationUser : IdentityUser<int>
{
    //public DateOnly DateOfBirth { get; set; }
    public DateTime Created { get; set; } = DateTime.UtcNow;
    //public DateTime LastActive { get; set; } = DateTime.UtcNow;
    public required List<Framework> Frameworks { get; set; }
    public ICollection<AppUserRole> UserRoles { get; set; } = [];
}