using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataServices.Model;
using Messages.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(
            IMapper mapper,
            UserManager<AppUser> userManager,
            RoleManager<Role> roleManager)
        {
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                return BadRequest(new { message = "Role name should be provided." });
            }

            var newRole = new Role
            {
                Name = roleName
            };

            var roleResult = await _roleManager.CreateAsync(newRole);

            if (roleResult.Succeeded)
            {
                return Ok();
            }

            return Problem(roleResult.Errors.First().Description, null, 500);
        }

        [HttpPost("User/Role")]
        public async Task<IActionResult> AddUserToRole(AddEmailToRoleRequest request)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == request.UserEmail);

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);

            if (result.Succeeded)
            {
                return Ok();
            }

            return Problem(result.Errors.First().Description, null, 500);
        }
    }
}
