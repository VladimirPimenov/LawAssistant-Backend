using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	internal class LawDocumentsRepository(
		PostgreSqlDbContext dbContext)
		: ILawDocumentsRepository
	{
		public async Task<List<LawAct>> GetAllActsAsync()
		{
			return await dbContext.LawAct
				.Include(act => act.Articles)
				.ToListAsync();
		}

		public async Task<ActArticle> GetArticleAsync(int articleId)
		{
			return await dbContext.ActArticle.FindAsync(articleId);
		}

		public async Task<LawAct> GetLawActAsync(int actId)
		{
			return await dbContext.LawAct
				.Include(act => act.Articles)
				.FirstOrDefaultAsync(act => act.ActId == actId);
		}
	}
}
