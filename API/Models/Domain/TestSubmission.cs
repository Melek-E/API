using System;
using System.Collections.Generic;
using API.Models.Domain.Questions;

namespace API.Models.Domain.Extra
{
    public class TestSubmission
    {
        public int Id { get; set; }
        public int TestId { get; set; }
        public DateTime SubmittedAt { get; set; }
        public double? Score { get; set; } // Nullable until graded

        public Test Test { get; set; } // Navigation property
        public List<SubmittedQuestion> SubmittedQuestions { get; set; } = new List<SubmittedQuestion>();
    }

    public class SubmittedQuestion
    {
        public int Id { get; set; }
        public int TestSubmissionId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; } // Store user's answer

        public TestSubmission TestSubmission { get; set; }
        public Question Question { get; set; }
    }
}
