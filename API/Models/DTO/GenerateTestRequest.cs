using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class GenerateTestRequest
    {
        [Required]
        public int Level { get; set; } 

        [Required]
        public int NumberOfQuestions { get; set; }



        [Required]
        public string userId { get; set; }
    }
}
