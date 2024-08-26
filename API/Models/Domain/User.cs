namespace API.Models.Domain
{
    public class User:Person
    {
        public string Score { get; set; }
        public string Level{ get; set; }
        public string Frameworks{ get; set; }



        public virtual ICollection<Answer> Answers{ get; set; } = new List<Answer>();

    }
}
