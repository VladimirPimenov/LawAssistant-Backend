using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class LawyerRepository(
		PostgreSqlDbContext dbContext)
		: ILawyerRepository
	{
		public async Task<Lawyer> CreateLawyerAsync(Lawyer lawyer)
		{
			dbContext.Lawyer.Add(lawyer);
			dbContext.SaveChangesAsync();

			return lawyer;
		}

		public async Task<Lawyer> GetLawyerAsync(int lawyerId)
		{
			return await dbContext.Lawyer.FindAsync(lawyerId);
		}
	}
}
