﻿using API.Models.Domain.Questions;

namespace API.Models.Domain
{

    public enum AnswerType
    {   Basic,
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

        public string UserId { get; set; }
        
    }
}
