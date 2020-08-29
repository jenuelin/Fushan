using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataServices.Model;
using DataServices.Services;
using Fushan.Extensions;
using Fushan.Helpers;
using Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController, Authorize]
    public class UserController : ControllerBase
    {
        private readonly IMember _member;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        //private readonly IHubContext<StronglyTypedChatHubBase, IChatClient> _hub;
        public UserController(IMember member, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _member = member;
        }
        // POST api/accounts
        [HttpPost, AllowAnonymous]
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

            return new OkObjectResult("Account created");
        }

        [HttpGet]
        public async Task<PaginatedList<AppUser>> GetAll(GetUsersRequest request)
        {
            var users = _userManager.Users.Where(request.UserID, x => x.UserID == request.UserID)
                .Where(request.UserName, x => x.UserName == request.UserName);
            users = users.OrderByDynamic(request.SortBy, request.IsDesc);
            return await PaginatedList<AppUser>.CreateAsync(users.AsNoTracking(), request.Page, request.Rows);
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

            return new OkObjectResult("Account deleted");
        }
    }
}
