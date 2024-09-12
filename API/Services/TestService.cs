﻿using API.Data;
using API.Models;
using API.Models.Domain.Extra;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace API.Services
{
    public class TestService
    {
        private readonly QuizzDbContext _context;

        public TestService(QuizzDbContext context)
        {
            _context = context;
        }

        public async Task<Test> GenerateRandomTest(int level, int numberOfQuestions)
        {
            // Fetch questions based on the level
            var questions = await _context.Questions
                .Where(q => q.Level == level)
                .OrderBy(q => Guid.NewGuid()) // Randomize
                .Take(numberOfQuestions)
                .ToListAsync();

            if (questions.Count == 0)
                throw new InvalidOperationException("No questions found for the specified level.");

            var test = new Test
            {
                Questions = questions,
                Timestamp = DateTime.UtcNow,
                UserId = "08266ada-eb8c-4402-9c96-3d5425626c9b",
                Score = null  // Initially, the test has no score
            };

            _context.Tests.Add(test);
            await _context.SaveChangesAsync();

            return test;
        }

        public async Task<Test> GetTestById(int testId)
        {
            return await _context.Tests
                                 .Include(t => t.Questions)
                                 .FirstOrDefaultAsync(t => t.Id == testId);
        }


        
    }




}
