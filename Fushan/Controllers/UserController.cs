using AutoMapper;
using AutoMapper.QueryableExtensions;
using DataServices.Model;
using Fushan.Extensions;
using Fushan.Helpers;
using Fushan.Mapping;
using Messages;
using Messages.User;
using Microsoft.AspNetCore.Authorization;
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
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        //private readonly IHubContext<StronglyTypedChatHubBase, IChatClient> _hub;
        public UserController(UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RegistrationRequest model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = _mapper.Map<RegistrationRequest, AppUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            //await _member.CreateMember(userIdentity.Id, model.Location);

            return new OkObjectResult(new { message = "Account created" });
        }

        [HttpGet]
        public async Task<MessageResponse> GetAll([FromQuery] GetUsersRequest request)
        {
            //TODO: 把config改成DI方式
            //var config = new MapperConfiguration(cfg => {
            //    cfg.AddProfile<MappingProfile>();
            //});
            var users = _userManager.Users.Where(request.UserID, x => x.UserId == request.UserID)
                .Where(request.UserName, x => x.UserName.Contains(request.UserName));
            users = users.OrderByDynamic(request.SortBy, request.IsDesc);
            var result = await PaginatedIQueryableExtensions<AppUser>.CreateAsync(users.AsNoTracking(), request.Page, request.Rows, request.ShowAll);
            var userMappers = await result.Item.ProjectTo<AppUserModel>(MappingProfile.Config).ToArrayAsync();

            return new AppUserResponse
            {
                Valid = true,
                Count = result.Count,
                PagePageIndex = result.PageIndex,
                TotalPages = result.TotalPages,
                Table = userMappers
            };
        }

        [HttpGet("{id}")]
        public async Task<AppUserModel> Get(Guid id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            return _mapper.Map<AppUser, AppUserModel>(user);
        }

        [HttpPut]
        public async Task<IActionResult> Update(CreateUpdateUserRequest request)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == request.Id);
            if (user == null)
            {
                throw new Exception("can't find user");
            }

            _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(user);
            return Ok(result.Succeeded);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            return new OkObjectResult(new { message = "Account deleted" });
        }
    }
}