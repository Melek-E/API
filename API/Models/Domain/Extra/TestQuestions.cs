using API.Models.Domain.Questions;
using API.Models.Domain.Extra;

namespace API.Models.Domain.Extra
{
    public class TestQuestion
    {
        public int TestId { get; set; }
        public Test Test { get; set; }

        public int QuestionId { get; set; }
        public Question Question { get; set; }

        public string UserAnswer { get; set; } // Field for the user's answer
    }
}
