using API.Models.Domain.Extra;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Auth
{
    public class EditDTO
    {
     
        [EmailAddress]
        
        public required string Email { get; set; }


        [DataType(DataType.Text)]
        public required string Username { get; set; }


        public required ICollection<Framework> Frameworks { get; set; }



    }
}