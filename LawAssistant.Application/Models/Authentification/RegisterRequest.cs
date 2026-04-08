namespace LawAssistant.Application.Models.Authentification
{
    public record RegisterRequest
    {
        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }

        public string Password { get; init; }
    }
}
