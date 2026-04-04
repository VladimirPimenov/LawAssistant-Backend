using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
    public class ComparisonRepository(
        PostgreSqlDbContext dbContext)
        : IComparisonRepository
    {
        public async Task<ComparisonResult> GetComparisonResultAsync(int resultId)
        {
            return await dbContext.ComparisonResult.FirstOrDefaultAsync(cr => cr.ResultId == resultId);
        }

        public async Task<int> CompareParagraphWithArticle(ContractParagraph paragraph, ActArticle article)
        {
            return await dbContext.CompareParagraphWithArticle(paragraph.ParagraphId, article.ArticleId);
        }
    }
}
