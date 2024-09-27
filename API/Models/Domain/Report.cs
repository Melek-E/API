using API.Models.Domain.Extra;
using API.Models.Domain.Questions;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain
{
    public class Report
    {
        public int Id { get; set; }  // Unique identifier for the report

        public int TestId { get; set; }  // Foreign key to Test
        public Test Test { get; set; }  // Navigation property

        public int QuestionId { get; set; }  // Foreign key to Question
        public Question Question { get; set; }  // Navigation property

        public bool IsCorrect { get; set; }  // Whether the answer is correct
        public double Score { get; set; }  // Score assigned by the admin
        public string Feedback { get; set; }  // Optional feedback from the admin
    }
}
