using LawAssistant.Infrastructure.RepositoryImplementation;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Infrastructure
{
    internal class SyntaxModuleClient(
        PostgreSqlDbContext dbContext) 
        : ISyntaxModuleApiClient
    {
        public async Task<int> MakeSyntaxComparisonAsync(ContractParagraph paragraph, ActArticle article)
        {
            return await dbContext.CompareParagraphWithArticle(paragraph.ParagraphId, article.ArticleId);
        }
    }
}
