using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
    public interface IComparisonRepository
    {
        public Task<ComparisonResult> GetComparisonResultAsync(int resultId);

        public Task<ComparisonResult> UpdateComparisonResultAsync(ComparisonResult result);

        public Task<int> RemoveComparisonResultAsync(ComparisonResult result);

        public Task<int> CompareParagraphWithArticle(ContractParagraph paragraph, ActArticle article);
    }
}
