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

            var question = await _context.Questions
                .Include(q => q.Answers)
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
                Question = question  
            };

            _context.Answer.Add(answer);
            question.Answers.Add(answer); 

            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAnswerById), new { id = answer.Id }, answer);
        }

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

        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] CreateAnswerDTO answerDto)
        {
            if (ModelState.IsValid)
            {
                var existingAnswer = await _context.Answer.FindAsync(id);
                if (existingAnswer == null)
                {
                    return NotFound();
                }

                existingAnswer.AnswerText = answerDto.AnswerText;
                existingAnswer.IsCorrect = answerDto.IsCorrect;
                existingAnswer.QuestionId = answerDto.QuestionId;
                existingAnswer.UserId = answerDto.userId;

                try
                {
                    _context.Update(existingAnswer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnswerExists(id))
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

        // DELETE: api/Answers

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
