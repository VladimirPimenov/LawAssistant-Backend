using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo
{
	public class LawyerRepository(
		PostgreSqlDbContext dbContext)
		: ILawyerRepository
	{
		public async Task<Lawyer> CreateLawyerAsync(Lawyer lawyer)
		{
			dbContext.Lawyer.Add(lawyer);
			await dbContext.SaveChangesAsync();

			return lawyer;
		}

		public async Task<Lawyer> GetLawyerByEmailAsync(string email)
		{
			return await dbContext.Lawyer.FirstOrDefaultAsync(l => l.Email == email);
		}

		public async Task<Lawyer> GetLawyerAsync(int lawyerId)
		{
			return await dbContext.Lawyer.FindAsync(lawyerId);
		}

		public async Task<Lawyer> UpdateLawyerAsync(Lawyer lawyer)
		{
			dbContext.Lawyer.Update(lawyer);
			await dbContext.SaveChangesAsync();

			return lawyer;
		}

		public async Task<List<Lawyer>> GetAllLawyersAsync()
		{
			return await dbContext.Lawyer.ToListAsync();
		}
	}
}
