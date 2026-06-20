using System.Security.Claims;

namespace LawAssistant.Api.Extensions
{
    public static class ClaimsExtension
    {
        public static int? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            var claim = claimsPrincipal.FindFirst("userId");
            
            return claim == null ? null: Convert.ToInt32(claim.Value);
        }
    }
}