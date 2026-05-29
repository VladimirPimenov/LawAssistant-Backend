using Microsoft.AspNetCore.Mvc;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;

namespace LawAssistant.Api.Controllers
{
    /// <summary>
    /// Контроллер для аутентификации пользователей
    /// </summary>
    [ApiController, Route("auth")]
    public class AuthentificationController(
        IAuthentificationService authService)
        : ControllerBase
    {
        /// <summary>
        /// Выполняет регистрацию пользователя
        /// </summary>
        /// <param name="registerRequest">Запрос на регистрацию</param>
        /// <returns>Ответ с данными зарегистрированного пользователя</returns>
        [HttpPost("register")]
        [ProducesResponseType<RegisterResponce>(200)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var registeredUser = await authService.RegisterAsync(registerRequest);

            return registeredUser == null ? BadRequest() : Ok(registeredUser);
        }

        /// <summary>
        /// Выполняет аутентификацию пользователя по логину и паролю
        /// </summary>
        /// <param name="loginRequest">Запрос на вход (логин и пароль)</param>
        /// <returns>Токен аутентификации</returns>
        [HttpPost("login")]
		[ProducesResponseType<string>(200)]
		[ProducesResponseType(401)]
		public async Task<IActionResult> LoginAsync(LoginRequest loginRequest)
        {
            var token = await authService.LoginAsync(loginRequest);

            if(token == null)
                return Unauthorized();

			HttpContext httpContext = ControllerContext.HttpContext;
            httpContext.Response.Cookies.Append("token", token);

            return Ok(token);
        }

        /// <summary>
        /// Выполняет выход из учётной записи
        /// </summary>
        [HttpPost("logout")]
		[ProducesResponseType(200)]
		public async Task<IActionResult> Logout()
        {
			HttpContext httpContext = ControllerContext.HttpContext;
            httpContext.Response.Cookies.Delete("token");

            return Ok();
        }
    }
}
