using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для работы с результатами сопоставления
    /// </summary>
    public interface IComparisonRepository
    {
        public Task<ComparisonResult> GetComparisonResultAsync(int resultId);

        public Task<ComparisonResult> CreateComparisonResultAsync(ComparisonResult result);

        public Task<ComparisonResult> UpdateComparisonResultAsync(ComparisonResult result);

        public Task<int> RemoveComparisonResultAsync(ComparisonResult result);
    }
}
