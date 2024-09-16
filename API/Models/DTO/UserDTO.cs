using API.Models.Domain.Extra;

namespace API.Models.DTO
{
    public class UserDTO
    {
        public required string Username { get; set; }
        public required string Email { get; set; }
        public string Id { get; set; }

        public required ICollection<Framework> Frameworks { get; set; }



    }

}