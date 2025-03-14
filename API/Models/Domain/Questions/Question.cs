﻿// Models/Domain/Question.cs    
using API.Models.Domain.Reports;
using System.ComponentModel.DataAnnotations;

namespace API.Models.Domain.Questions
{

   
    public class Question
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }

//        public virtual Admin Admin { get; set; }
        public string UserId { get; set; }
        
        [Range(1, 3, ErrorMessage = "Level must be between 1 and 3.")]
        public int Level { get; set; }

        public int AnswerId {  get; set; }

        public virtual ICollection<Answer> Answers { get; set; }


        public List<QuestionAssessment> QuestionAssessments { get; set; } = new List<QuestionAssessment>();  // Navigation property for assessments



        private string _type = default!;

        // Using 'required' ensures that 'Type' must be set during object initialization
        public required string QuestionType
        {
            get => _type;
            set => _type = value switch
            {
                "MultipleChoice" or "TrueFalse" or "Basic" => value,
                _ => throw new ArgumentException("Type must be 'MultipleChoice', 'TrueFalse', or 'Basic'")
            };
        }
    }

    


}
    