using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using API.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using API.Models.Domain.Auth;
using API.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public ProfileController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null) return NotFound();

            var userDTO = new UserDTO
            {
                Username = user.UserName,
                Email = user.Email
            };

            return Ok(userDTO);
        }

        [HttpPut]
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
