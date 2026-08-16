namespace LawAssistant.Application.Models
{
    public record AccountDto
    {
        public int AccountId { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }
    }
}
