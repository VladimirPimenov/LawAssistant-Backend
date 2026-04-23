using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Models
{
    public record ResultDto
    {
        public int ResultId { get; init; }

        public ActArticle Article { get; init; }

        public string Text { get; init; }

        public float MatchValue { get; init; }
    }
}
