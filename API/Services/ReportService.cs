using API.Data;
using API.Models.Domain;
using API.Models.Domain.Extra;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        public async Task<Report> CreateReportAsync(Report report)
        {
            _context.Reports.Add(report);
            await _context.SaveChangesAsync();
            return report;
        }

        // Get all reports for a specific test
        public async Task<List<Report>> GetReportsByTestIdAsync(int testId)
        {
            return await _context.Reports
                .Include(r => r.Question)  // Include the related Question
                .Where(r => r.TestId == testId)
                .ToListAsync();
        }
    }
}
