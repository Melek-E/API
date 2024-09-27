using System;
using System.Collections.Generic;
using API.Models.Domain.Auth;
using API.Models.Domain.Questions;
using API.Models.Domain.Reports;

namespace API.Models.Domain.Extra
{
    public class Test
    {
        public int Id { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.Now;  // Automatically sets the time when the test is created
        public double? Score { get; set; }  // Nullable score, can be updated later

        public string? UserId { get; set; }  // Foreign key to ApplicationUser

        public List<Question> Questions { get; set; } = new List<Question>();


        public virtual Report Report { get; set; }  // Navigation property to Report

    }
}
