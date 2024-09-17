using API.Models;
using API.Models.Domain.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

using Microsoft.AspNetCore.Authorization;
using API.Data;


namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly QuizzDbContext _context;


        public UsersController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, QuizzDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

       [HttpGet]
public IActionResult GetUsers()
{
    var usersfram = _userManager.Users
        .Select(user => new 
        {
            user.Id,
            user.UserName,
            user.Email,
            Frameworks = user.Frameworks.Select(d => d.Name).ToList() // Will return an empty list if no frameworks are present

        })
        .ToList();

    return Ok(usersfram);
}



        // Get a specific user by ID
        [HttpGet("{id}")]
        public IActionResult GetUser(string id)
        {
            // Fetch the user by ID
            var user = _userManager.Users
                .Where(u => u.Id == id)
                .Select(user => new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    Frameworks = user.Frameworks.Select(d => d.Name).ToList() // Will return an empty list if no frameworks are present
                })
                .FirstOrDefault();

            // Check if user was found
            if (user == null)
            {
                return NotFound(); // Return 404 if user not found
            }

            return Ok(user); // Return the user profile if found
        }

        // Get all users with the 'admin' role
        [HttpGet("Admins")]
        public async Task<IActionResult> GetAdmins()
        {
            var role = await _roleManager.FindByNameAsync("Admin");
            if (role == null)
            {
                return NotFound("Role 'admin' not found.");
            }

            var usersInAdminRole = await _userManager.GetUsersInRoleAsync("admin");

            return Ok(usersInAdminRole);
        }


        [HttpGet("AllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var usersInUserRole = await _userManager.GetUsersInRoleAsync("User");
            return Ok(usersInUserRole);
        }
        //// Create a new user
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromBody] CreateUserModel model)
        //{
        //    var frameworks = model.Frameworks;

        //    var user = new ApplicationUser { UserName = model.UserName, Email = model.Email, 
        //        Frameworks= frameworks};
        //    var result = await _userManager.CreateAsync(user, model.Password);

        //    if (result.Succeeded)
        //    {
        //        return Ok(user.Frameworks);
        //    }

        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error.Description);
        //    }
        //    return BadRequest(ModelState);
        //}

        // Update user information
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = model.UserName;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return Ok(user);
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }

        // Delete a user
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }

        // Assign a user to a role (e.g., Admin)
        [Authorize(Roles = "SuperAdmin")]

        [HttpPost("{id}/assign-role")]
        public async Task<IActionResult> AssignRole(string id, [FromBody] AssignRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roleExists = await _roleManager.RoleExistsAsync(model.Role);
            if (!roleExists)
            {
                return BadRequest("Role does not exist");
            }

            var result = await _userManager.AddToRoleAsync(user, model.Role);
            if (result.Succeeded)
            {
                return Ok();
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
            return BadRequest(ModelState);
        }
    }
}
