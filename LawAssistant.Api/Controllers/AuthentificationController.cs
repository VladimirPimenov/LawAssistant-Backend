using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;

namespace LawAssistant.Api.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthentificationController(
        IAuthentificationService authService)
        : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var registeredUser = await authService.RegisterAsync(registerRequest);

            return registeredUser == null ? Results.BadRequest() : Results.Ok(registeredUser);
        }

        [HttpPost("login")]
        public async Task<IResult> LoginAsync(LoginRequest loginRequest)
        {
            var token = await authService.LoginAsync(loginRequest);

            if(token == null)
                return Results.Unauthorized();

            var httpContext = ControllerContext.HttpContext;
            httpContext.Response.Cookies.Append("token", token);

            return Results.Ok(token);
        }
    }
}
