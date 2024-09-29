namespace API.Models.DTO
{
    public enum AnswerType
    {
        Basic,
        Choice,
        MultipleChoice,
        TrueFalse
    }
    public class CreateAnswerDTO
    {
        public string AnswerText { get; set; }
        public string userId { get; set; }

        public int QuestionId { get; set; }

        public AnswerType Type { get; set; }

        public bool IsCorrect{ get; set; }
    }

}
