using System;
using System.Collections.Generic;

namespace API.Models.Domain
{
    public class Test
    {
        public int Id { get; set; }
        public DateTime DateHourTaken { get; set; }
        public int Score { get; set; }
        public int AdminId { get; set; }

        // List of Question IDs
        public List<int> QuestionIds { get; set; } = new List<int>();

        public Test(int id, DateTime dateHourTaken, int score, int adminId, List<int> questionIds)
        {
            Id = id;
            DateHourTaken = dateHourTaken;
            Score = score;
            AdminId = adminId;
            QuestionIds = questionIds;
        }
    }
}
