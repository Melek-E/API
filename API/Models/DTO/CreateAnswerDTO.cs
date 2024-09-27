namespace API.Models.DTO
{
    public class CreateAnswerDTO
    {
        public string AnswerText { get; set; }
        public string userId { get; set; }

        public int QuestionId { get; set; }
    }

}
