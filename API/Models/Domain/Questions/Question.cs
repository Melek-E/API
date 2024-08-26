﻿// Models/Domain/Question.cs
namespace API.Models.Domain.Questions
{

    public enum QuestionType
    {
        Choice,
        MultipleChoice,
        TrueFalse
    }
    public class Question
    {
        public int Id { get; set; }
        public string? QuestionText { get; set; }
        public required Admin Admin { get; set; }

        public int AdminId { get; set; }
        public string? Level { get; set; }
        public QuestionType Type { get; set; }
    }

   
}
