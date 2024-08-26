using API.Models.Domain.Questions;

namespace API.Models.Domain
{
    public class Admin:User
    {
        public bool HasSuperAdminPrivileges { get; set; }

        public virtual ICollection<Question> Questions { get; set; } = new List<Question>();


    }
}
