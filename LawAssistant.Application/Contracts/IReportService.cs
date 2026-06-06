using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Contracts
{
	/// <summary>
	/// Сервис для работы с отчётами о сопоставлении договора с законодательными актами
	/// </summary>
	public interface IReportService
	{
		/// <summary>
		/// Возвращает отчёт
		/// </summary>
		/// <param name="reportId">Идентификатор отчёта</param>
		/// <returns>Найденный отчёт</returns>
		public Task<ReportDetail> GetReportAsync(int reportId);

		/// <summary>
		/// Возвращает список отчётов по договору
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Список отчётов</returns>
		public Task<List<ReportDto>> GetContractReportsAsync(int contractId);

		/// <summary>
		/// Возвращает список отчётов юриста
		/// </summary>
		/// <param name="lawyerId">Идентификатор юриста</param>
		/// <returns>Список отчётов</returns>
		public Task<List<ReportDto>> GetLawyerReportsAsync(int lawyerId);

		/// <summary>
		/// Создаёт отчёт о сопоставлении договора с законодательными актами
		/// </summary>
		/// <param name="contractId">Идентификатор договора, для которого формируется отчёт</param>
		/// <returns>Созданный отчёт</returns>
		public Task<ReportDetail> CreateReportAsync(int contractId);

		/// <summary>
		/// Удаляет отчёт
		/// </summary>
		/// <param name="reportId">Идентификатор отчёта</param>
		/// <returns>Идентификатор удалённого отчёта</returns>
		public Task<int?> RemoveReportAsync(int reportId);
	}
}
