using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
    public interface IComparisonService
    {
        public Task<ComparisonResult> GetComparisonResultAsync(int resultId);

        public Task<int?> RemoveComparisonResultAsync(int resultId);

        public Task<List<ComparisonResult>> MakeSyntacticComparisonAsync(List<ContractParagraph> paragraphs);
        
        public Task MakeSemanticComparisonAsync(List<ComparisonResult> syntacticComparisonResults);

    }
}
