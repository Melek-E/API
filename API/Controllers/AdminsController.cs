using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Data;
using API.Models.Domain;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminsController : ControllerBase
    {
        private readonly QuizzDbContext _context;

        public AdminsController(QuizzDbContext context)
        {
            _context = context;
        }

        // GET: api/Admins
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Admin>>> GetAdmins()
        {
            return Ok(await _context.Admins.ToListAsync());
        }

        // GET: api/Admins/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Admin>> GetAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);

            if (admin == null)
            {
                return NotFound();
            }

            return Ok(admin);
        }

        // POST: api/Admins
        [HttpPost]
        public async Task<ActionResult<Admin>> CreateAdmin([Bind("HasSuperAdminPrivileges,Score,Level,frameworks,Id,Name,email,PasswordHash")] Admin admin)
        {
            if (ModelState.IsValid)
            {
                _context.Admins.Add(admin);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetAdmin), new { id = admin.Id }, admin);
            }

            return BadRequest(ModelState);
        }

        // PUT: api/Admins/5
        [HttpPut("{id}")]
        public async Task<IActionResult> EditAdmin(int id, [Bind("HasSuperAdminPrivileges,Score,Level,frameworks,Id,Name,email,PasswordHash")] Admin admin)
        {
            if (id != admin.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Entry(admin).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdminExists(id))
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

        // DELETE: api/Admins/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            var admin = await _context.Admins.FindAsync(id);
            if (admin == null)
            {
                return NotFound();
            }

            _context.Admins.Remove(admin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminExists(int id)
        {
            return _context.Admins.Any(e => e.Id == id);
        }
    }
}
