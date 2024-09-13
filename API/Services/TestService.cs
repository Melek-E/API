using API.Data;
using API.Models.Domain.Extra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
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
            var questions = await _context.Questions
                .Where(q => q.Level == level)
                .OrderBy(q => Guid.NewGuid())
                .Take(numberOfQuestions)
                .ToListAsync();

            if (questions.Count == 0)
                throw new InvalidOperationException("No questions found for the specified level.");

            var test = new Test
            {
                Questions = questions,
                Timestamp = DateTime.UtcNow,
                UserId = "08266ada-eb8c-4402-9c96-3d5425626c9b",
                Score = null
            };

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

        public async Task<List<Test>> GetAllTests()
        {
            return await _context.Tests
                                 .Include(t => t.Questions)
                                 .ToListAsync();
        }

        public async Task<List<Test>> GetTestsByUserId(string userId)
        {
            return await _context.Tests
                                 .Include(t => t.Questions)
                                 .Where(t => t.UserId == userId)
                                 .ToListAsync();
        }
    }
}
