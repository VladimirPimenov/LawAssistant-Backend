using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class LawDocumentsRepository(
		PostgreSqlDbContext dbContext)
		: ILawDocumentsRepository
	{
		public async Task<ActArticle> GetArticleAsync(int articleId)
		{
			return await dbContext.ActArticle.FindAsync(articleId);
		}

		public async Task<LawAct> GetLawActAsync(int actId)
		{
			return await dbContext.LawAct.FindAsync(actId);
		}
	}
}
