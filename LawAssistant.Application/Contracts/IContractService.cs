using LawAssistant.Domain.Entities;
using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
	/// <summary>
	/// Сервис для работы с коллективными договорами
	/// </summary>
	public interface IContractService
	{
		/// <summary>
		/// Возвращет список договоров юриста
		/// </summary>
		/// <param name="lawyerId">Идентификатор юриста</param>
		/// <returns>Список договоров</returns>
		public Task<List<ContractDto>> GetLawyerContractsInfoAsync(int lawyerId);

		/// <summary>
		/// Возвращает модель с основной информацией о договоре
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Найденный договор</returns>
		public Task<ContractDto> GetContractAsync(int contractId);

		/// <summary>
		/// Возвращает договор с абзацами
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Найденный договор</returns>
		public Task<CollectiveContract> GetContractWithParagraphsAsync(int contractId);

		/// <summary>
		/// Создает договор
		/// </summary>
		/// <param name="contractRequest">Модель с информацией для создания договора</param>
		/// <returns>Созданный договор</returns>
		public Task<CollectiveContract> CreateContractAsync(CreateContractRequest contractRequest);

		/// <summary>
		/// Изменяет договор
		/// </summary>
		/// <param name="contractDto">Модель договора с обновлёнными полями</param>
		/// <returns>Изменённый договор</returns>
		public Task<CollectiveContract> UpdateContractAsync(ContractDto contractDto);

		/// <summary>
		/// Удаляет договор
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Идентификатор удалённого договора</returns>
		public Task<int?> RemoveContractAsync(int contractId);
	}
}
