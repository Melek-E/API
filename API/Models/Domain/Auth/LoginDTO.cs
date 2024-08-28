using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain.Auth
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public required string PasswordHash { get; set; }

        
    }
}
