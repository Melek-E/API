using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models.Domain.Extra;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FrameworksController : ControllerBase
    {
        private readonly QuizzDbContext _context; //

        public FrameworksController(QuizzDbContext context)
        {
            _context = context;
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
    }
}
