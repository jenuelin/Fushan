using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Fushan.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CultureController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { cookieName = HttpContext.Request.Cookies["Culture"] });
        }

        [HttpPost("{culture}")]
        public IActionResult SetLanguage(string culture)
        {
            Response.Cookies.Append(
                "Culture",
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return Ok();
        }
    }
}