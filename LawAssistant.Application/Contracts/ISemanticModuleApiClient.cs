using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface ISemanticModuleApiClient
	{
		public Task<ComparisonResult> CompareWithEmbeddingAsync(ComparisonResult comparisonResult);
	}
}
