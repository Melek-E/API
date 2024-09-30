// DTOs/QuestionUpdateDto.cs
namespace API.DTOs
{
    public class QuestionUpdateDto
    {
        public string? QuestionText { get; set; }
        public int Level { get; set; }
        public int AnswerId { get; set; }
        public string? QuestionType { get; set; }
    }
}
