using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using API.Models.DTOs.Auth;
using API.Models.Domain.Auth;
using API.Models.DTO;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.Models.Domain.Extra; // For Framework
using API.Services;
using Microsoft.AspNetCore.Authorization; 



namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IFrameworkService _frameworkService;

        public AuthController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IFrameworkService frameworkService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _frameworkService = frameworkService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _signInManager.PasswordSignInAsync(loginDto.Email, loginDto.PasswordHash, isPersistent: false, lockoutOnFailure: false);

            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid login attempt!" });

            // Retrieve user information after successful login
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return NotFound(new { message = "User not found!" });

            // Map user to UserDTO
            var userDto = new UserDTO
            {
                Username = user.UserName,
                Email = user.Email,
                Frameworks= user.Frameworks
            };

            return Ok(new
            {
                message = "Login successful!",
                user = userDto
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDTO registerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var frameworks = await _frameworkService.GetOrCreateFrameworksAsync(registerDto.Frameworks);

            var user = new ApplicationUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                Frameworks = frameworks
            };

            var result = await _userManager.CreateAsync(user, registerDto.PasswordHash);

            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok(new { message = "Registration successful!" ,frameworks});
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok(new { message = "Logout successful!" });
        }

        [HttpPost("toggle-role")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> ToggleUserRole(string userId)
        {
            var currentUser = await _userManager.GetUserAsync(User);
           
            
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found.");
            }

            var currentRoles = await _userManager.GetRolesAsync(user);
            string newRole = currentRoles.Contains("Admin") ? "User" : "Admin";

            if (currentRoles.Contains("SuperAdmin"))
            {
                return Unauthorized(new { message = "Impossible to change Super Admin Role (temp solution?)" });
            }

            if (currentRoles.Contains("Admin"))
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            else
            {
                await _userManager.RemoveFromRoleAsync(user, "User");
            }

            await _userManager.AddToRoleAsync(user, newRole);

            return Ok($"User role changed to {newRole}.");
        }

        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userDTO = new UserDTO
            {   
                Username = user.UserName,
                Email = user.Email,
                Id=user.Id,
                Frameworks=user.Frameworks
            };

            return Ok(userDTO);
        }

        [HttpPut("profile")]
        [Authorize]
        public async Task<IActionResult> UpdateProfile([FromBody] UserDTO userDTO)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.UserName = userDTO.Username;
            user.Email = userDTO.Email;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(userDTO);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }
    }
}
