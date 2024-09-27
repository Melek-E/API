using API.Data;
using API.Hubs;
using API.Models.Domain.Extra;
using API.Models.DTO;
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
                UserId = userId,
                Timestamp = DateTime.UtcNow,
                Score = null
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            await _hubContext.Clients.User(userId).SendAsync("ReceiveTestNotification", "A new test has been created for you!");

            return test;
        }

        public async Task<TestDTO> GetTestDTOByIdAsync(int testId, bool includeReport = false)
        {
            var test = await _context.Tests
                                 .Include(t => t.Questions)
                                 .FirstOrDefaultAsync(t => t.Id == testId);

            if (test == null)
                return null;

            var testDTO = new TestDTO
            {
                Id = test.Id,
                Timestamp = test.Timestamp,
                Score = test.Score,
                UserId = test.UserId,
                Questions = test.Questions.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.QuestionText,
                    Level = q.Level
                }).ToList()
            };

            if (includeReport)
            {
                var report = await _context.Reports
                    .Include(r => r.QuestionAssessments)
                    .FirstOrDefaultAsync(r => r.TestId == testId);

                if (report != null)
                {
                    testDTO.Report = new ReportDTO
                    {
                        Id = report.Id,
                        Score = report.Score,
                        QuestionAssessments = report.QuestionAssessments.Select(qa => new QuestionAssessmentDTO
                        {
                            Id = qa.Id,
                            IsCorrect = qa.IsCorrect,
                            PointsAwarded = qa.PointsAwarded,
                            QuestionId = qa.QuestionId
                        }).ToList()
                    };
                }
            }

            return testDTO;
        }

        public async Task<List<TestDTO>> GetAllTestsDTOAsync()
        {
            var tests = await _context.Tests
                                 .Include(t => t.Questions)
                                 .ToListAsync();

            return tests.Select(test => new TestDTO
            {
                Id = test.Id,
                Timestamp = test.Timestamp,
                Score = test.Score,
                UserId = test.UserId,
                Questions = test.Questions.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.QuestionText,
                    Level = q.Level
                }).ToList()
            }).ToList();
        }

        public async Task<List<TestDTO>> GetTestsDTOByUserIdAsync(string userId)
        {
            var tests = await _context.Tests
                                 .Include(t => t.Questions)
                                 .Where(t => t.UserId == userId)
                                 .ToListAsync();

            return tests.Select(test => new TestDTO
            {
                Id = test.Id,
                Timestamp = test.Timestamp,
                Score = test.Score,
                UserId = test.UserId,
                Questions = test.Questions.Select(q => new QuestionDTO
                {
                    Id = q.Id,
                    Text = q.QuestionText,
                    Level = q.Level
                }).ToList()
            }).ToList();
        }
    }
}
