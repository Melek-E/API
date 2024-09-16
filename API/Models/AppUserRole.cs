using API.Models;
using Microsoft.AspNetCore.Identity;

namespace API.Models;

public class AppUserRole : IdentityUserRole<int>
{
    public ApplicationUser User { get; set; } = null!;
    public AppRole Role { get; set; } = null!;
}