using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string Password { get; set; }


        public string? Username { get; set; }


    }
}
