using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	public interface ILawDocumentsService
	{
		public Task<ActArticle> GetArticleAsync(int articleId);

		public Task<LawAct> GetLawActAsync(int actId);
	}
}
