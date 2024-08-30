using Microsoft.AspNetCore.Mvc;
using API.Services;
using System.Threading.Tasks;
using API.Models.Domain.Extra;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        private readonly TestService _testService;

        public TestsController(TestService testService)
        {
            _testService = testService;
        }

        // GET: api/Tests/generate
        [HttpGet("generate")]
        [Authorize]
        public async Task<ActionResult<Test>> GenerateTest([FromQuery] string level, [FromQuery] int numberOfQuestions)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);  // Get the current user ID
            var test = await _testService.GenerateRandomTest(level, numberOfQuestions, userId);

            if (test.Questions.Count == 0)
            {
                return NotFound("No questions available for the specified level.");
            }

            return Ok(test);
        }

        // GET: api/Tests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Test>> GetTestById(int id)
        {
            var test = await _testService.GetTestById(id);

            if (test == null)
            {
                return NotFound("Test not found.");
            }

            return Ok(test);
        }

        // POST: api/Tests/{id}/score
        [HttpPost("{id}/score")]
        public async Task<IActionResult> UpdateTestScore(int id, [FromBody] double score)
        {
            await _testService.UpdateTestScore(id, score);

            return Ok(new { message = "Score updated successfully." });
        }
    }
}
