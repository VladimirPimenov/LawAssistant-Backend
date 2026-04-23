using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
    public record ReportParagraph
    {
        public ContractParagraph Paragraph { get; init; }

        public List<ResultDto> ComparisonResults { get; init; }
    }
}
