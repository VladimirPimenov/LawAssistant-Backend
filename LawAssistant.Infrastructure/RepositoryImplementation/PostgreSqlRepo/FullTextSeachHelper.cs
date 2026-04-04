using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public static class FullTextSeachHelper
	{
		private static readonly string _compareFunctionName = "\"CompareParagraphWithArticle\"";

		public static async Task<int> CompareParagraphWithArticle(
			this PostgreSqlDbContext dbContext,
			int paragraphId, int articleId)
		{
			var sql = $"SELECT * FROM {_compareFunctionName}({paragraphId}, {articleId}) AS \"Value\"";
			return await dbContext.Database.SqlQueryRaw<int>(sql).FirstOrDefaultAsync();
		}
	}
}
