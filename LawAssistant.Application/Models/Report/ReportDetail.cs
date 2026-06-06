namespace LawAssistant.Application.Models
{
    public record ReportDetail
    {
        public int ReportId { get; init; }

        public DateTime ReportedDate { get; init; }

        public int ContractId { get; init; }

        public List<ParagraphMatches> Results { get; init; }
    }
}
