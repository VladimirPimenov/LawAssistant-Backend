using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface ILawDocumentsRepository
	{
		public Task<LawAct> GetLawActAsync(int actId);

		public Task<ActArticle> GetArticleAsync(int articleId);

		public Task<List<LawAct>> GetAllActsAsync();
	}
}
