using API.Models.Domain.Extra;
using Microsoft.AspNetCore.Identity;

namespace API.Models.Domain.Auth
{
    public class ApplicationUser: IdentityUser
    {
        
        public virtual required ICollection<Framework> Frameworks { get; set; }


        

    }
}
