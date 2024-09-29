using API.Data;
using API.Models.Domain.Reports;
using API.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace API.Services
{
    public class ReportService
    {
        private readonly QuizzDbContext _context;

        public ReportService(QuizzDbContext context)
        {
            _context = context;
        }

        // Create a new report
        public async Task<Report> CreateReportAsync(ReportDTO reportDto)
        {
            var test = await _context.Tests
                                     .Include(t => t.Questions)
                                     .FirstOrDefaultAsync(t => t.Id == reportDto.TestId);

            if (test == null)
                throw new InvalidOperationException("Test not found.");

            // Create a new report
            var report = new Report
            {
                TestId = reportDto.TestId,
                AdminId = reportDto.AdminId,
                Score = reportDto.Score,
                ReviewedAt = DateTime.Now,
                QuestionAssessments = reportDto.QuestionAssessments.Select(qa => new QuestionAssessment
                {
                    IsCorrect = qa.IsCorrect,
                    Feedback = qa.Feedback
                }).ToList()
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return report;
        }

        // Retrieve report by test ID
        public async Task<Report?> GetReportByTestIdAsync(int testId)
        {
            return await _context.Reports
                                 .Include(r => r.QuestionAssessments)
                                 .Include(r => r.Test)
                                 .FirstOrDefaultAsync(r => r.TestId == testId);
        }
    }
}
