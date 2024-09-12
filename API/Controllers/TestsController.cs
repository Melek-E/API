using API.Models;
using API.Models.Domain.Extra;
using API.Models.DTO;
using API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<ActionResult<Test>> GenerateTest([FromBody] GenerateTestRequest request)
        {
            // Log the received JSON request
            Console.WriteLine($"Received JSON: {System.Text.Json.JsonSerializer.Serialize(request)}");

            try
            {
                var test = await _testService.GenerateRandomTest(request.Level, request.NumberOfQuestions);
                return Ok(test);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
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
    }
}
