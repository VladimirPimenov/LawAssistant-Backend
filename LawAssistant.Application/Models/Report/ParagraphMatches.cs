using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
    public record ParagraphMatches
    {
        public ContractParagraph Paragraph { get; init; }

        public List<ArticleMatch> ComparisonResults { get; init; }
    }
}
