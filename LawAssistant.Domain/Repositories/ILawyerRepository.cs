using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	public interface ILawyerRepository
	{
		public Task<Lawyer> GetLawyerAsync(int lawyerId);

		public Task<Lawyer> CreateLawyerAsync(Lawyer lawyer);
	}
}
