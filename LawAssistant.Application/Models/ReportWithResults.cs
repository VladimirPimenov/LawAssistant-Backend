namespace LawAssistant.Application.Models
{
    public record ReportWithResults
    {
        public int ReportId { get; init; }

        public DateTime ReportedDate { get; init; }

        public int ContractId { get; init; }

        public List<ReportParagraph> Results { get; init; }
    }
}
