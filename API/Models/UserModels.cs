using API.Models.Domain.Extra;

namespace API.Models
{
    public class CreateUserModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required List<Framework> Frameworks{ get; set; }


    }

    public class UpdateUserModel
    {
        public string UserName { get; set; }
        public List<Framework> Frameworks { get; set; }

    }

    public class AssignRoleModel
    {
        public string Role { get; set; }
    }
}
