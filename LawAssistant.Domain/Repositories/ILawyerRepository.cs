using LawAssistant.Domain.Entities;

namespace LawAssistant.Domain.Repositories
{
	/// <summary>
	/// Репозиторий для работы с юристами
	/// </summary>
	public interface ILawyerRepository
	{
		public Task<Lawyer> GetLawyerAsync(int lawyerId);

		public Task<Lawyer> GetLawyerByEmailAsync(string email);

		public Task<List<Lawyer>> GetAllLawyersAsync();

		public Task<Lawyer> CreateLawyerAsync(Lawyer lawyer);

		public Task<Lawyer> UpdateLawyerAsync(Lawyer lawyer);
	}
}
