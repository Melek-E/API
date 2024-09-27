using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models.Domain;
using API.Models.DTO;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswersController : ControllerBase
    {
        private readonly QuizzDbContext _context;

        public AnswersController(QuizzDbContext context)
        {
            _context = context;
        }

        // GET: api/Answers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Answer>>> Index()
        {
            return await _context.Answer.ToListAsync();
        }

        // GET: api/Answers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Answer>> Details(int id)
        {
            var answer = await _context.Answer.FindAsync(id);

            if (answer == null)
            {
                return NotFound();
            }

            return answer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnswer([FromBody] CreateAnswerDTO answerDto)
        {
            // Validate the incoming answer DTO
            if (answerDto == null || string.IsNullOrWhiteSpace(answerDto.AnswerText) || answerDto.QuestionId <= 0 || string.IsNullOrWhiteSpace(answerDto.userId))
            {
                return BadRequest("Invalid answer data.");
            }

            // Retrieve the Question from the database
            var question = await _context.Questions
                .Include(q => q.Answers)  // Include answers to avoid lazy loading issues
                .FirstOrDefaultAsync(q => q.Id == answerDto.QuestionId);

            if (question == null)
            {
                return NotFound("Question not found.");
            }

            // Map DTO to entity
            var answer = new Answer
            {
                AnswerText = answerDto.AnswerText,
                QuestionId = answerDto.QuestionId,
                UserId = answerDto.userId,
                Question = question  // Include the full Question entity
            };

            // Add the answer to the context and also add it to the Question's answer collection
            _context.Answer.Add(answer);
            question.Answers.Add(answer); // This line adds the answer to the Question's collection

            await _context.SaveChangesAsync();

            // Return the created answer along with a 201 status code
            return CreatedAtAction(nameof(GetAnswerById), new { id = answer.Id }, answer);
        }

        // Optional: GET method to retrieve an answer by ID
        [HttpGet("answerid")]
        public async Task<IActionResult> GetAnswerById(int id)
        {
            var answer = await _context.Answer.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            return Ok(answer);
        }


        // PUT: api/Answers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [Bind("Id,AnswerText,IsCorrect,Type,QuestionId,UserId")] Answer answer)
        {
            if (id != answer.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(answer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(answer.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        // DELETE: api/Answers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var answer = await _context.Answer.FindAsync(id);
            if (answer == null)
            {
                return NotFound();
            }

            _context.Answer.Remove(answer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AnswerExists(int id)
        {
            return _context.Answer.Any(e => e.Id == id);
        }
    }
}
