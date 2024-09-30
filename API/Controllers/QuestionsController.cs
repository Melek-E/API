using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using API.Models.Domain.Questions;
using API.Services;
using API.Data;
using Microsoft.EntityFrameworkCore;
using API.DTOs;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuestionsController : ControllerBase
    {
        private readonly QuestionService _questionService;
        private readonly QuizzDbContext _context;


        public QuestionsController(QuestionService questionService, QuizzDbContext context)
        {
            _questionService = questionService;
            _context= context;

        }

        // GET: api/Questions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Question>>> GetQuestions()
        {
            var questions = await _questionService.GetQuestionsAsync();
            return Ok(questions);
        }


        // GET: api/Questions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Question>> GetQuestion(int id)
        {
            // Eager load the answers when fetching the question
            var question = await _context.Questions
                .Include(q => q.Answers)  // This will include the Answers collection
                .FirstOrDefaultAsync(q => q.Id == id);

            if (question == null)
            {
                return NotFound();
            }

            return Ok(question);
        }

        // POST: api/Questions
        [HttpPost]
        public async Task<ActionResult<Question>> PostQuestion(Question question)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdQuestion = await _questionService.CreateQuestionAsync(question);

            return CreatedAtAction(nameof(GetQuestion), new { id = createdQuestion.Id }, createdQuestion);
        }

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateQuestion(int id, [FromBody] QuestionUpdateDto questionDto)
        //{
        //    if (questionDto == null)
        //    {
        //        return BadRequest("Question data is required.");
        //    }

        //    // Retrieve the question by ID, including related answers
        //    var existingQuestion = await _context.Questions
        //        .Include(q => q.Answers)  // Include the Answers collection to ensure it's loaded
        //        .FirstOrDefaultAsync(q => q.Id == id);

        //    if (existingQuestion == null)
        //    {
        //        return NotFound("Question not found.");
        //    }

        //    // Update only the allowed fields (but not the Answers collection)
        //    existingQuestion.QuestionText = questionDto.QuestionText;
        //    existingQuestion.Level = questionDto.Level;
        //    existingQuestion.AnswerId = questionDto.AnswerId;
        //    existingQuestion.QuestionType = existingQuestion.QuestionType;

        //    // Save the changes to the database
        //    try
        //    {
        //        _context.Questions.Update(existingQuestion);
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!QuestionExists(id))
        //        {
        //            return NotFound("Question no longer exists.");
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();  // Return 204 No Content on success
        //}
        //// Helper method to check if a question exists
        private bool QuestionExists(int id)
        {
            return _context.Questions.Any(q => q.Id == id);
        }



        // DELETE: api/Questions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteQuestion(int id)
        {
            var deleted = await _questionService.DeleteQuestionAsync(id);
            if (!deleted)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
