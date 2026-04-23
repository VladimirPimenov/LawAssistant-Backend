namespace LawAssistant.Domain.Entities
{
    public class ComparisonResult
    {
        public int ResultId { get; set; }

        public int ArticleId { get; set; }

        public int ParagraphId { get; set;  }

        public ContractParagraph ContractParagraph { get; set; }

        public string Text { get; set; }

        public float MatchValue { get; set; }
    }
}
