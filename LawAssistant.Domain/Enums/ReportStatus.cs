using System.ComponentModel;

namespace LawAssistant.Domain.Enums
{
    /// <summary>
    /// Статус проверки коллективного договора
    /// </summary>
    public enum ReportStatus
    {
        /// <summary>
        /// Договор не проверен
        /// </summary>
        [Description("Идёт проверка")]
        InProgress = 0,
        
        /// <summary>
        /// Выполняется синтаксическое сопоставление
        /// </summary>
        [Description("Идёт синтаксическая проверка")]
        SyntaxProcessing = 1,
        
        /// <summary>
        /// Выполнено синтаксическое сопоставление
        /// </summary>
        [Description("Выполнена синтаксическая проверка")]
        SyntaxChecked = 2,
        
        /// <summary>
        /// Выполняется семантическое сопоставление
        /// </summary>
        [Description("Идёт семантическая проверка")]
        SemanticProcessing = 3,
        
        /// <summary>
        /// Выполнено семантическое сопоставление
        /// </summary>
        [Description("Выполнена семантическая проверка")]
        SemanticChecked = 4,
        
        /// <summary>
        /// Ошибка при сопоставлении
        /// </summary>
        [Description("Произошла ошибка")]
        CheckError = 5
    }
}