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
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;



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

            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null)
                return NotFound(new { message = "User not found!" });


            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            //Assert.False(await _userManager.IsLockedOutAsync(user));


            var result = await _signInManager.PasswordSignInAsync(user.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: false);

            
            if (!result.Succeeded)
                return Unauthorized(new { message = "Invalid login attempt 7!" });



            // Map user to UserDTO
            var userDto = new UserDTO
            {
                Username = user.UserName,
                Email = user.Email,
            };

            return Ok(new
            {
                message = "Login successful!",
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
                Frameworks = frameworks,
                EmailConfirmed = true

            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

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
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (userId == null) return Unauthorized();

            //var user = await _userManager.FindByIdAsync(userId);
            //if (user == null) return NotFound();

            var user = await _userManager.Users
                 .Where(u => u.Id == userId)
                 .Select(user => new
                 {
                     user.Id,
                     user.UserName,
                     user.Email,
                     Frameworks = user.Frameworks.Select(d => d.Name).ToList() // Will return an empty list if no frameworks are present
                 })
     .FirstOrDefaultAsync();

            if (user == null) return NotFound();


            return Ok(user);
        }





        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] EditDTO editdto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            user.UserName = editdto.Username;
            user.Email = editdto.Email;

            //user.NormalizedUserName = _userManager.NormalizeName(editdto.Username);
            //user.NormalizedEmail = _userManager.NormalizeEmail(editdto.Email);

            user.EmailConfirmed = true;

            // Update the concurrency stamp

            //user.ConcurrencyStamp = Guid.NewGuid().ToString();
            //await _userManager.UpdateSecurityStampAsync(user);




            await _frameworkService.RemoveFrameworksForUserAsync(userId);

            var frameworks = await _frameworkService.GetOrCreateFrameworksAsync(editdto.Frameworks);
            user.Frameworks = frameworks;


            

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                // Sign out the user to refresh claims
                return Ok(editdto);
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    Console.WriteLine(error.Description);
                }

                return BadRequest(result.Errors);
            }
        }




    }
}












//[HttpPatch("api/users/{userId}")]
//public async Task<IActionResult> PatchUser(string userId, [FromBody] EditDTO patchDto)
//{
//    if (patchDto == null)
//    {
//        return BadRequest("Invalid data.");
//    }

//    // Get the user from the database using UserManager
//    var user = await _userManager.FindByIdAsync(userId);
//    if (user == null)
//    {
//        return NotFound($"User with ID {userId} not found.");
//    }

//    // Update Username if provided
//    if (!string.IsNullOrWhiteSpace(patchDto.Username))
//    {
//        user.UserName = patchDto.Username;
//        user.NormalizedUserName = _userManager.NormalizeName(patchDto.Username);
//    }

//    // Update Email if provided
//    if (!string.IsNullOrWhiteSpace(patchDto.Email))
//    {
//        user.Email = patchDto.Email;
//        user.NormalizedEmail = _userManager.NormalizeEmail(patchDto.Email);
//    }

//    // Update Frameworks if provided
//    if (patchDto.Frameworks != null && patchDto.Frameworks.Any())
//    {
//        // Use your existing service to get or create frameworks (returns ICollection<Framework>)
//        var frameworks = await _frameworkService.GetOrCreateFrameworksAsync(patchDto.Frameworks);

//        // Assuming the user entity has a collection of Framework objects
//        user.Frameworks = frameworks;
//    }

//    // Save changes to the database
//    var result = await _userManager.UpdateAsync(user);
//    if (!result.Succeeded)
//    {
//        return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update user.");
//    }

//    return Ok(user);
//}