using API.Data;
using API.Models.Domain.Questions;
using Microsoft.EntityFrameworkCore;

namespace API.Services
{
    public class QuestionService
    {
        private readonly QuizzDbContext _context;

        public QuestionService(QuizzDbContext context)
        {
            _context = context;
        }

        // Get all questions
        public async Task<IEnumerable<Question>> GetQuestionsAsync()
        {
            return await _context.Questions
                .Include(q => q.Answers)  
                .ToListAsync();
        }

        // Get question by ID
        public async Task<Question?> GetQuestionByIdAsync(int id)
        {
            return await _context.Questions
                .Include(q => q.Answers) 
                .FirstOrDefaultAsync(q => q.Id == id); 
        }

        public async Task<Question> CreateQuestionAsync(Question question)
        {
            _context.Questions.Add(question);
            await _context.SaveChangesAsync();
            return question;
        }

        // Update an existing question
        public async Task<bool> UpdateQuestionAsync(Question question)
        {
            _context.Entry(question).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await QuestionExistsAsync(question.Id))  // Use the correct property
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }
        }

        // Delete a question
        public async Task<bool> DeleteQuestionAsync(int id)
        {
            var question = await _context.Questions.FindAsync(id);
            if (question == null)
            {
                return false;
            }

            _context.Questions.Remove(question);
            await _context.SaveChangesAsync();
            return true;
        }

        // Check if question exists
        private async Task<bool> QuestionExistsAsync(int id)
        {
            return await _context.Questions.AnyAsync(e => e.Id == id);  // Use the correct property
        }
    }
}
