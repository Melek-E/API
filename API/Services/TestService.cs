using API.Data;
using API.Models;
using API.Models.Domain.Extra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TestService
    {
        private readonly QuizzDbContext _context;

        public TestService(QuizzDbContext context)
        {
            _context = context;
        }

        public async Task<Test> GenerateRandomTest(int level, int numberOfQuestions)
        {
            // Fetch questions based on the level
            var questions = await _context.Questions
                .Where(q => q.Level == level)
                .OrderBy(q => Guid.NewGuid()) // Randomize
                .Take(numberOfQuestions)
                .ToListAsync();

            if (questions.Count == 0)
                throw new InvalidOperationException("No questions found for the specified level.");

            var test = new Test
            {
                Questions = questions,
                Timestamp = DateTime.UtcNow
            };

            return test;
        }
    }
}
