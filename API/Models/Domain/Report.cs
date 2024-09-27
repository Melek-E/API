using System;
using System.Collections.Generic;
using API.Models.Domain.Extra;
using API.Models.Domain.Questions;  // For referencing Test entity

namespace API.Models.Domain.Reports
{
    public class Report
    {
        public int Id { get; set; }  // Report ID
        public int TestId { get; set; }  // Foreign key to the Test
        public Test Test { get; set; }  // Navigation property for the Test

        public string AdminId { get; set; }  // The admin who created the report
        public DateTime ReviewedAt { get; set; } = DateTime.Now;  // When the test was reviewed

        public double Score { get; set; }  // The final score assigned to the test

        public List<QuestionAssessment> QuestionAssessments { get; set; } = new List<QuestionAssessment>();  // Holds individual question corrections
    }

    public class QuestionAssessment
    {
        public int Id { get; set; }

        public Question Question { get; set; }  // Navigation to the Question being assessed

        public int QuestionId { get; set; }  // Foreign key to the Question
        public bool IsCorrect { get; set; }  // Whether the answer is correct or not
        public string Feedback { get; set; }  // Optional feedback for the answer

        public Report Report { get; set; }
        public int PointsAwarded{ get; set; }

        public int ReportId { get; set; }
    }
}
