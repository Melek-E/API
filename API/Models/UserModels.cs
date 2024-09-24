using API.Models.Domain.Extra;

namespace API.Models
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public  required ICollection<Framework> Frameworks{ get; set; }


    }



    public class AssignRoleModel
    {
        public string Role { get; set; }
    }
}
