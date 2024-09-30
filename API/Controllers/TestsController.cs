using API.Models.DTO;
using API.Models.Domain.Extra;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

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

        // POST: api/Tests/GenerateTest
        [HttpPost("GenerateTest")]
        public async Task<ActionResult<TestDTO>> GenerateTest([FromBody] GenerateTestRequest request)
        {
            try
            {
                //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);


                var test = await _testService.GenerateRandomTest(request.Level, request.NumberOfQuestions, request.userId);

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

                return Ok(testDTO);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET: api/Tests/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<TestDTO>> GetTestById(int id, [FromQuery] bool includeReport = false)
        {
            var testDTO = await _testService.GetTestDTOByIdAsync(id, includeReport);

            if (testDTO == null)
            {
                return NotFound("Test not found.");
            }

            return Ok(testDTO);
        }

        // GET: api/Tests
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TestDTO>>> GetAllTests()
        {
            var testsDTO = await _testService.GetAllTestsDTOAsync();
            return Ok(testsDTO);
        }

        // GET: api/Tests/User/{userId}
        [HttpGet("User/{userId}")]
        public async Task<ActionResult<IEnumerable<TestDTO>>> GetTestsByUserId(string userId)
        {
            var testsDTO = await _testService.GetTestsDTOByUserIdAsync(userId);

            if (testsDTO == null || testsDTO.Count == 0)
            {
                return NotFound($"No tests found for UserId: {userId}");
            }

            return Ok(testsDTO);
        }
    }
}
