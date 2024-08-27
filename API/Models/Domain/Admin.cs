using API.Models.Domain.Questions;

namespace API.Models.Domain
{
    public class Admin:Person
    {
        public bool HasSuperAdminPrivileges { get; set; }

        public virtual ICollection<Question>? Questions { get; set; } = new List<Question>();


    }
}

//https://stackoverflow.com/questions/3509026/how-should-i-represent-a-unique-super-admin-privilege-in-a-database