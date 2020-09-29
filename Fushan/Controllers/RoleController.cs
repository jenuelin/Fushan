using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataServices.Model;
using Fushan.Extensions;
using Fushan.Mapping;
using Messages.Role;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

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

        [HttpGet]
        public async Task<RoleResponse> GetAll([FromQuery] GetRolesRequest request)
        {
            var roles = _roleManager.Roles.Where(request.Id, x => x.Id == request.Id)
                .Where(request.Name, x => x.Name.Contains(request.Name));
            roles = roles.OrderByDynamic(request.SortBy, request.IsDesc);
            var result = await PaginatedIQueryableExtensions<Role>.CreateAsync(roles.AsNoTracking(), request.Page, request.Rows, request.ShowAll);
            var roleMappers = await result.Item.ProjectTo<RoleModel>(MappingProfile.Config).ToArrayAsync();

            return new RoleResponse
            {
                Valid = true,
                Count = result.Count,
                PagePageIndex = result.PageIndex,
                TotalPages = result.TotalPages,
                Table = roleMappers
            };
        }

        [HttpGet("{id}")]
        public async Task<RoleModel> Get(Guid id)
        {
            var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<Role, RoleModel>(role);
        }

        [HttpPost("Roles")]
        public async Task<IActionResult> CreateRole([FromForm] string roleName)
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