using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	/// <summary>
	/// Репозиторий для работы с законодательными актами
	/// </summary>
	public interface ILawDocumentsRepository
	{
		public Task<LawAct> GetActAsync(int actId);

		public Task<ActArticle> GetArticleAsync(int articleId);

		public Task<List<LawAct>> GetAllActsAsync();
	}
}
