using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Клиент API модуля синтаксического анализа
    /// </summary>
    public interface ISyntaxModuleApiClient 
    {
        /// <summary>
        /// Выполняет сопоставление фрагмента текста со статьёй
        /// </summary>
        /// <param name="paragraph">Фрагмент текста</param>
        /// <param name="article">Статья</param>
        /// <returns>Идентификатор результата сопоставления</returns>
        public Task<int> MakeSyntaxComparisonAsync(ContractParagraph paragraph, ActArticle article);
    }
}