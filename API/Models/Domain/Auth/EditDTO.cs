using API.Models.Domain.Extra;
using System.ComponentModel.DataAnnotations;

namespace API.Models.DTOs.Auth
{
    public class EditDTO
    {
     
        [EmailAddress]
        
        public  string Email { get; set; }


        [DataType(DataType.Text)]
        public  string Username { get; set; }


        public  ICollection<Framework> Frameworks { get; set; }



    }
}