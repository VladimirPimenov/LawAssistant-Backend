using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    /// <summary>
    /// Сервис сопоставления фрагментов договора с законодательством
    /// </summary>
    public interface IComparisonService
    {
        /// <summary>
        /// Возвращает результат сравнения
        /// </summary>
        /// <param name="resultId">Идентификатор результата</param>
        /// <returns>Результат сопоставления</returns>
        public Task<ComparisonResult> GetComparisonResultAsync(int resultId);

        /// <summary>
        /// Удаляет результат сравнения
        /// </summary>
        /// <param name="resultId">Идентификатор результата</param>
        /// <returns>Идентификатор удалённого результата</returns>
        public Task<int?> RemoveComparisonResultAsync(int resultId);

        /// <summary>
        /// Выполняет синтаксическое сопоставление фрагментов договора с законодательством
        /// </summary>
        /// <param name="paragraphs">Фрагменты договора</param>
        /// <returns>Результаты синтаксического сопоставления</returns>
        public Task<List<ComparisonResult>> MakeSyntacticComparisonAsync(List<ContractParagraph> paragraphs);
        
        /// <summary>
        /// Выполняет семантическое сопоставление фрагментов договора с законодательством
        /// </summary>
        /// <param name="paragraphs">Результаты синтаксического сопоставления</param>
        public Task MakeSemanticComparisonAsync(List<ComparisonResult> syntacticComparisonResults);

    }
}
