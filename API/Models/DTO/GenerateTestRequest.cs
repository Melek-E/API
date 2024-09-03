using System.ComponentModel.DataAnnotations;

namespace API.Models.DTO
{
    public class GenerateTestRequest
    {
        [Required]
        public int Level { get; set; } // Ensure this matches the expected type in your JSON

        [Required]
        public int NumberOfQuestions { get; set; }
    }
}
