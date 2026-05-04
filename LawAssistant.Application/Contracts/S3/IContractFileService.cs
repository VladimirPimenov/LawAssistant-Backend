using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts
{
	public interface IContractFileService
	{
		public Task<Guid> SaveContractFileAsync(IFormFile contractFile);

		public Task<IFormFile> LoadContractFileAsync(int contractId);
	}
}
