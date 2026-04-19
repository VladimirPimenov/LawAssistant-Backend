namespace LawAssistant.Application.Models
{
    public record ReportDto
    {
        public int ReportId { get; init;  }

        public DateTime ReportedDate { get; init; }

        public ContractDto Contract { get; init; }
    }
}
