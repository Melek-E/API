using API.Models.Domain.Reports;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<Report>> CreateReport([FromBody] ReportDTO reportDto)
        {
            try
            {
                var report = await _reportService.CreateReportAsync(reportDto);
                return Ok(report);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Reports/{testId}
        [HttpGet("{testId}")]
        public async Task<ActionResult<Report>> GetReportByTestId(int testId)
        {
            var report = await _reportService.GetReportByTestIdAsync(testId);

            if (report == null)
            {
                return NotFound("Report not found.");
            }

            return Ok(report);
        }
    }
}
