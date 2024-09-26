using API.Data;
using API.Hubs;
using API.Models.Domain.Extra;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TestService
    {
        private readonly QuizzDbContext _context;
        private readonly IHubContext<TestNotificationHub> _hubContext;

        public TestService(QuizzDbContext context, IHubContext<TestNotificationHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        public async Task<Test> GenerateRandomTest(int level, int numberOfQuestions, string userId)
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
                UserId = userId,  // Set the UserId here
                Timestamp = DateTime.UtcNow,
                Score = null
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            // Notify the user via SignalR
            await _hubContext.Clients.User(userId).SendAsync("ReceiveTestNotification", "A new test has been created for you!");

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
