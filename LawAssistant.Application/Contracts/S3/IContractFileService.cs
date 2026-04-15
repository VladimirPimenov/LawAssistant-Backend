using Microsoft.AspNetCore.Http;

namespace LawAssistant.Application.Contracts.S3
{
	public interface IContractFileService
	{
		public Task<Guid> SaveContractFileAsync(IFormFile contractFile);

		public Task<IFormFile> LoadContractFileAsync(int contractId);
	}
}
