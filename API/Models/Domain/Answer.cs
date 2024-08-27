using API.Models.Domain.Questions;

namespace API.Models.Domain
{

    public enum AnswerType
    {
        Choice,
        MultipleChoice,
        TrueFalse
    }
    public class Answer
    {
        

        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }

        public AnswerType Type { get; set; }




        public int QuestionId { get; set; }

        public int UserId { get; set; }
        
    }
}
