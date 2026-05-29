using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	/// <summary>
	/// Клиент API модуля семантического анализа
	/// </summary>
	public interface ISemanticModuleApiClient
	{
		/// <summary>
		/// Для результатов синтаксического анализа выполняет семантическое сопоставление с указанными статьями
		/// </summary>
		/// <param name="comparisonResult">Результаты синтаксического сопоставления</param>
		/// <returns>Результаты семантического сопоставлния</returns>
		public Task<ComparisonResult> CompareWithEmbeddingAsync(ComparisonResult comparisonResult);
	}
}
