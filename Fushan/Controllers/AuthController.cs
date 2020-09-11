using AutoMapper;
using DataServices.Model;
using DataServices.Services;
using Fushan.Helpers;
using Messages;
using Messages.Auth;
using Messages.User;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        private readonly JwtSettings _jwtSettings;
        private readonly IMember _member;

        public AuthController(IMember member, IMapper mapper, UserManager<AppUser> userManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _mapper = mapper;
            _member = member;
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
        }

        // POST api/accounts
        [HttpPost("SignUp")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginRequest request)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == request.Email || u.UserName == request.Email);
            if (user is null)
            {
                return NotFound(new { message = "User not found" });
            }

            var userSigninResult = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userSigninResult)
            {
                var roles = await _userManager.GetRolesAsync(user);
                return Ok(new
                {
                    Id = user.Id,
                    Username = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = GenerateJwt(user, roles)
                });
            }

            return BadRequest(new { message = "Email or password incorrect." });
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            _ = HttpContext.SignOutAsync().ConfigureAwait(false);
            return Ok("logout");
        }

        [Authorize]
        [HttpGet("CheckAuth")]
        public IActionResult CheckAuth()
        {
            return Ok("CheckAuth");
        }

        [HttpGet("CheckAuth2")]
        public IActionResult CheckAuth2()
        {
            return Ok("CheckAuth2");
        }

        private string GenerateJwt(AppUser user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_jwtSettings.ExpirationInDays));

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Issuer,
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}