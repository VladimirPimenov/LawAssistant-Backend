using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	internal class ComparisonRepository(
        PostgreSqlDbContext dbContext)
        : IComparisonRepository
    {
        public async Task<ComparisonResult> GetComparisonResultAsync(int resultId)
        {
            return await dbContext.ComparisonResult.FirstOrDefaultAsync(cr => cr.ResultId == resultId);
        }

		public Task<ComparisonResult> CreateComparisonResultAsync(ComparisonResult result)
        {
            throw new NotImplementedException();
        }

		public async Task<ComparisonResult> UpdateComparisonResultAsync(ComparisonResult updatedResult)
		{
			dbContext.ComparisonResult.Update(updatedResult);
			await dbContext.SaveChangesAsync();

			return updatedResult;
		}

		public async Task<int> RemoveComparisonResultAsync(ComparisonResult result)
		{
			dbContext.ComparisonResult.Remove(result);
			await dbContext.SaveChangesAsync();

			return result.ResultId;
		}
    }
}
