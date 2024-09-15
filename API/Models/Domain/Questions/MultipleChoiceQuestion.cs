namespace API.Models.Domain.Questions
{
    
    public class MultipleChoiceQuestion : Question
    {
        public List<string> Choices { get; set; }
    }
}

