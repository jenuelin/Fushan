using System;
using System.Threading.Tasks;
using AutoMapper;
using DataServices.Model;
using DataServices.Services;
using Messages;
using Messages.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IDepartment _department;

        public DepartmentController(IDepartment department, IMapper mapper, UserManager<AppUser> userManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _department = department;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateDepartmentRequest model)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var department = _mapper.Map<CreateUpdateDepartmentRequest, Department>(model);
            department.CreatedByUsername = user.UserName;
            department.UpdatedByUsername = user.UserName;
            department.CreatedOn = DateTimeOffset.Now;
            department.UpdatedOn = DateTimeOffset.Now;

            await _department.CreateDepartment(department);

            return new OkObjectResult("Account created");
        }
    }
}
