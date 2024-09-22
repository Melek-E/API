﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models.Domain.Extra;
using API.Services;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrameworksController : ControllerBase
    {
        private readonly QuizzDbContext _context;
        private readonly IFrameworkService _frameworkService;

        public FrameworksController(QuizzDbContext context, IFrameworkService frameworkService)
        {
            _context = context;
            _frameworkService = frameworkService;
        }

        // GET: api/Frameworks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Framework>>> GetFrameworks()
        {
            return await _context.Frameworks.ToListAsync();
        }

        // GET: api/Frameworks/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Framework>> GetFramework(int id)
        {
            var framework = await _context.Frameworks.FindAsync(id);

            if (framework == null)
            {
                return NotFound();
            }

            return framework;
        }
        // Post api/Frameworks
        [HttpPost]
        public async Task<IActionResult> AddFrameworks([FromBody] ICollection<Framework> frameworks)
        {
            if (frameworks == null || frameworks.Count == 0)
            {
                return BadRequest("Frameworks collection cannot be null or empty.");
            }

            var result = await _frameworkService.GetOrCreateFrameworksAsync(frameworks);

            return Ok(result);
        }

        // POST: api/Frameworks/choose
        [HttpPost("choose")]
        public async Task<IActionResult> ChooseFramework([FromBody] List<int> frameworkIds)
        {
            // Retrieve the frameworks from the database
            var frameworks = await _context.Frameworks.Where(f => frameworkIds.Contains(f.Id)).ToListAsync();

            if (frameworks.Count != frameworkIds.Count)
            {
                return NotFound("One or more frameworks not found.");
            }

            // Store the selected framework IDs in the session
            HttpContext.Session.SetString("SelectedFrameworkIds", string.Join(",", frameworkIds));

            return Ok(new { message = $"Frameworks '{string.Join(", ", frameworks.Select(f => f.Name))}' selected successfully." });
        }

        // GET: api/Frameworks/selected
        [HttpGet("selected")]
        public async Task<IActionResult> GetSelectedFrameworks()
        {
            var frameworkIdsString = HttpContext.Session.GetString("SelectedFrameworkIds");

            if (string.IsNullOrEmpty(frameworkIdsString))
            {
                return NotFound("No frameworks selected.");
            }

            var frameworkIds = frameworkIdsString.Split(',').Select(int.Parse).ToList();
            var frameworks = await _context.Frameworks.Where(f => frameworkIds.Contains(f.Id)).ToListAsync();

            if (!frameworks.Any())
            {
                return NotFound("No frameworks found.");
            }

            return Ok(frameworks);
        }
    }
}
