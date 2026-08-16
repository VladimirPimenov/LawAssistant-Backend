using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Services
{
    public class AuthentificationService(
        IHashService hashService,
        ITokenProvider tokenProvider,
        IAccountService accountService,
        IHttpContextAccessor httpContextAccessor) 
        : IAuthentificationService
    {
        public async Task<AccountDto> RegisterAsync(RegisterRequest registerRequest)
        {
            string hashedPassword = hashService.Hash(registerRequest.Password);

            var newUser = new Account
            {
                FirstName = registerRequest.FirstName,
                LastName = registerRequest.LastName,
                Email = registerRequest.Email,
                HashedPassword = hashedPassword
            };

            var registeredUser = await accountService.CreateAccountAsync(newUser);
            if (registeredUser == null)
                return null;

            return new AccountDto
            {
                AccountId = registeredUser.AccountId,
                FirstName = registeredUser.FirstName,
                LastName = registeredUser.LastName,
                Email = registeredUser.Email
            };
        }

        public async Task<AccountDto> LoginAsync(LoginRequest loginRequest)
        {
            var user = await accountService.GetAccountByEmailAsync(loginRequest.Email);

            if (user == null)
                return null;

            if (!hashService.Verify(loginRequest.Password, user.HashedPassword))
                return null;

            var token = tokenProvider.GenerateToken(user);
            
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.None,
                Expires = DateTime.UtcNow.AddHours(1)
            };
            
            var httpContext = httpContextAccessor.HttpContext;
            httpContext?.Response.Cookies.Append("token", token, cookieOptions);

            return new AccountDto
            {
                AccountId = user.AccountId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email
            };
        }

        public void Logout()
        {
            var httpContext = httpContextAccessor.HttpContext;
            httpContext?.Response.Cookies.Delete("token");
        }
    }
}
