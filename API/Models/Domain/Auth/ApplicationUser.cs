using API.Models.Domain.Extra;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain.Auth
{
    public class ApplicationUser: IdentityUser
    {

        public int FrameworkId { get; set; }

        public Framework? Framework { get; set; }

    }
}
