namespace LawAssistant.Domain.Entities
{
    public class ComparisonResult
    {
        public int ResultId { get; set; }

        public int ParagraphId { get; set; }

        public int ArticleId { get; set; }

        public string Text { get; set; }
    }
}
