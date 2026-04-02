namespace LawAssistant.Application.Models
{
    public record ContractDto
    {
        public int ContractId { get; init; }

        public string Title { get; init; }

        public DateTime CreatedDate { get; init; }

        public List<LawyerDto> Authors { get; init; }
    }
}
