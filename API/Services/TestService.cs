using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models.Domain.Extra;

namespace API.Services
{
    public class TestService
    {
        private readonly QuizzDbContext _context;

        public TestService(QuizzDbContext context)
        {
            _context = context;
        }

        public async Task<Test> GenerateRandomTest(string level, int numberOfQuestions, string userId)
        {
            // Fetch all questions of the specified level
            var questions = await _context.Questions
                                          .Where(q => q.Level == level)
                                          .ToListAsync();

            // Shuffle the list and take the required number of questions
            var random = new Random();
            var randomQuestions = questions.OrderBy(x => random.Next()).Take(numberOfQuestions).ToList();

            // Create a new Test instance
            var test = new Test
            {
                UserId = userId,
                Questions = randomQuestions,
                Timestamp = DateTime.Now
            };

            // Save the test to the database
            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return test;
        }

        public async Task<Test> GetTestById(int testId)
        {
            return await _context.Tests
                                 .Include(t => t.Questions)
                                 .FirstOrDefaultAsync(t => t.Id == testId);
        }

        public async Task UpdateTestScore(int testId, double score)
        {
            var test = await _context.Tests.FindAsync(testId);
            if (test != null)
            {
                test.Score = score;
                await _context.SaveChangesAsync();
            }
        }
    }
}
