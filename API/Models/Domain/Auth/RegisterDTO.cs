using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Auth
{
    public class RegisterDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string PasswordHash { get; set; }

        [Required]
        [Compare("PasswordHash")]
        public string ConfirmPassword { get; set; }


        [DataType(DataType.Text)]
        public string Username { get; set; }



        public int? FrameworkId { get; set; }


    }
}