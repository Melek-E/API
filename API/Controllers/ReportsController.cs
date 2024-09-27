using API.Models.Domain;
using API.Models.Domain.Extra;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly ReportService _reportService;

        public ReportsController(ReportService reportService)
        {
            _reportService = reportService;
        }

        // POST: api/Reports
        [HttpPost]
        public async Task<ActionResult<Report>> CreateReport([FromBody] Report report)
        {
            if (report == null)
            {
                return BadRequest("Invalid report data.");
            }

            var createdReport = await _reportService.CreateReportAsync(report);
            return CreatedAtAction(nameof(GetReportsByTestId), new { testId = createdReport.TestId }, createdReport);
        }

        // GET: api/Reports/Test/{testId}
        [HttpGet("Test/{testId}")]
        public async Task<ActionResult<List<Report>>> GetReportsByTestId(int testId)
        {
            var reports = await _reportService.GetReportsByTestIdAsync(testId);

            if (reports == null || reports.Count == 0)
            {
                return NotFound($"No reports found for TestId: {testId}");
            }

            return Ok(reports);
        }
    }
}
