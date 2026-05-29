using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
	/// <summary>
	/// Сервис для инкапсуляции логики работы с S3 хранилищем договоров
	/// </summary>
	public interface IContractFileService
	{
		/// <summary>
		/// Сохраняет файл договора в S3 хранилище
		/// </summary>
		/// <param name="contractFile">Файл договора</param>
		/// <returns>Ключ файла в S3 хранилище</returns>
		public Task<Guid> SaveContractFileAsync(IFormFile contractFile);

		/// <summary>
		/// Возвращает файл договора из S3 хранилища
		/// </summary>
		/// <param name="contractId">Идентификатор договора</param>
		/// <returns>Файл договора</returns>
		public Task<IFormFile> LoadContractFileAsync(int contractId);
	}
}
