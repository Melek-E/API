namespace API.Models
{
    public class CreateUserModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int FrameworkId { get; set; }


    }

    public class UpdateUserModel
    {
        public string UserName { get; set; }
        public int FrameworkId { get; set; }

    }

    public class AssignRoleModel
    {
        public string Role { get; set; }
    }
}
