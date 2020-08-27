using System.Threading.Tasks;
using AutoMapper;
using DataServices.Model;
using DataServices.Services;
using Fushan.Helpers;
using Messages;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IMember _member;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        //private readonly IHubContext<StronglyTypedChatHubBase, IChatClient> _hub;
        public AccountController(IMember member, UserManager<AppUser> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _member = member;
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

            return new OkObjectResult("Account created");
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
