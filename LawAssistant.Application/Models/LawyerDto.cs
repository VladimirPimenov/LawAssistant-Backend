namespace LawAssistant.Application.Models
{
    public record LawyerDto
    {
        public int LawyerId { get; init; }

        public string FirstName { get; init; }

        public string LastName { get; init; }

        public string Email { get; init; }
    }
}
