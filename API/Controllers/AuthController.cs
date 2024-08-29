using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.Auth;
using API.Models.Domain.Auth;
using API.Data; // Assuming QuizzDbContext is in the API.Data namespace
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly QuizzDbContext _context; // Add context for accessing frameworks

        public AuthController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, QuizzDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context; // Initialize context
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid login attempt!" });

            return Ok(new { message = "Login successful!" });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verify that the framework exists
            //var framework = await _context.Frameworks.FirstOrDefaultAsync(f => f.Id == registerDto.FrameworkId);
            //if (framework == null)
            //{
            //    return BadRequest(new { message = "Invalid framework selected." });
            //}

            var user = new ApplicationUser
            {
                UserName = registerDto.Email, // Identity requires UserName, so use email for UserName
                Email = registerDto.Email,
                FrameworkId = (int)registerDto.FrameworkId // Assign the selected framework
            };

            var result = await _userManager.CreateAsync(user, registerDto.PasswordHash);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Registration successful!" });
        }
    }
}
