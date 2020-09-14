using AutoMapper;
using AutoMapper.QueryableExtensions;
using Contracts;
using DataServices.Model;
using DataServices.Services;
using Fushan.Extensions;
using Fushan.Mapping;
using Messages;
using Messages.Auth;
using Messages.Department;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IDepartment _department;
        private readonly ILoggerManager _logger;

        public DepartmentController(
            IDepartment department, 
            IMapper mapper, 
            UserManager<AppUser> userManager, 
            IOptionsSnapshot<JwtSettings> jwtSettings,
            ILoggerManager logger)
        {
            _mapper = mapper;
            _department = department;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateUpdateDepartmentRequest model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var department = _mapper.Map<CreateUpdateDepartmentRequest, Department>(model);
            department.CreatedByUsername = user.UserName;
            department.UpdatedByUsername = user.UserName;
            department.CreatedOn = DateTimeOffset.Now;
            department.UpdatedOn = DateTimeOffset.Now;

            await _department.CreateDepartmentAsync(department);

            return new OkObjectResult(new { message = "Account created" });
        }

        [HttpGet]
        public async Task<DepartmentResponse> GetAll([FromQuery] GetDepartmentsRequest request)
        {
            var departments = _department.GetDepartmentsQuery().Where(request.DepartmentId, x => x.DepartmentId == request.DepartmentId)
                .Where(request.Name, x => x.Name.Contains(request.Name));
            departments = departments.OrderByDynamic(request.SortBy, request.IsDesc);
            var result = await PaginatedIQueryableExtensions<Department>.CreateAsync(departments.AsNoTracking(), request.Page, request.Rows, request.ShowAll);
            var userMappers = await result.Item.ProjectTo<DepartmentModel>(MappingProfile.Config).ToArrayAsync();

            return new DepartmentResponse
            {
                Valid = true,
                Count = result.Count,
                PagePageIndex = result.PageIndex,
                TotalPages = result.TotalPages,
                table = userMappers
            };
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var department = await _department.GetDepartmentAsync(id,x => x.Include(d => d.AppUsers));
            if(department.AppUsers.Count > 0)
            {
                //return new BadRequestObjectResult(new { message = "請先移除部門所有人員才能刪除部門" });
            }
            await _department.DeleteDepartmentAsync(id);
            return new OkObjectResult(new { message = "Department deleted" });
        }
    }
}