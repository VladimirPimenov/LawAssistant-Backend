namespace LawAssistant.Domain.Entities
{
    /// <summary>
    /// Результат сравнения абзаца договора со статьёй
    /// </summary>
    public class ComparisonResult
    {
        /// <summary>
        /// Идентификатор результата
        /// </summary>
        public int ResultId { get; set; }

        /// <summary>
        /// Идентификатор статьи законодательного акта
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Идентификатор абзаца коллективного договора (навигационное свойство)
        /// </summary>
        public int ParagraphId { get; set;  }

        /// <summary>
        /// Абзац коллективного договора
        /// </summary>
        public ContractParagraph ContractParagraph { get; set; }

        /// <summary>
        /// Текст абзаца с выделенными совпадениями
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Величина совпадения (дробная)
        /// </summary>
        public float MatchValue { get; set; }
    }
}
