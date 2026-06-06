namespace LawAssistant.Application.Models
{
    public record ArticleMatch
    {
        public int ResultId { get; init; }

        public ArticleWithAct Article { get; init; }

        public string Text { get; init; }

        public float MatchValue { get; init; }
    }
}
