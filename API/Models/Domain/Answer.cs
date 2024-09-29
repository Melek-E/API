using API.Models.Domain.Questions;
using Newtonsoft.Json;

namespace API.Models.Domain
{

   
    public class Answer
    {
        

        public int Id { get; set; }

        public string AnswerText { get; set; }

        public bool IsCorrect { get; set; }





        public int QuestionId { get; set; }



        public virtual Question Question { get; set; }

        public string UserId { get; set; }
        
    }
}
