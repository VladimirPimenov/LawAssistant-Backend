using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models.Authentification;

namespace LawAssistant.Api.Controllers
{
    [ApiController, Route("auth")]
    public class AuthentificationController(
        IAuthentificationService authService)
        : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var registeredUser = await authService.RegisterAsync(registerRequest);

            return registeredUser == null ? BadRequest() : Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var token = await authService.LoginAsync(loginRequest);

            if(token == null)
                return Unauthorized();

			HttpContext httpContext = ControllerContext.HttpContext;
            httpContext.Response.Cookies.Append("token", token);

            return Ok(token);
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
			HttpContext httpContext = ControllerContext.HttpContext;
            httpContext.Response.Cookies.Delete("token");

            return Ok();

        }
    }
}
