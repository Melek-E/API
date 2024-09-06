using System.Collections.Generic;

namespace API.Models.DTO
{
    public class TestSubmissionRequest
    {
        public int TestId { get; set; }
        public List<SubmittedQuestionDto> SubmittedQuestions { get; set; } = new List<SubmittedQuestionDto>();
    }

    public class SubmittedQuestionDto
    {
        public int QuestionId { get; set; }
        public string Answer { get; set; }
    }
}
