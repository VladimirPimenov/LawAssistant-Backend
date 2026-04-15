using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts.S3;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	public class ContractFileService(
		IS3Adapter s3Adapter,
		IContractRepository contractRepository)
		: IContractFileService
	{
		public async Task<IFormFile> LoadContractFileAsync(int contractId)
		{
			var contract = await contractRepository.GetContractAsync(contractId);
			if (contract == null)
				return null;

			var fileKey = contract.FileKey;
			if (fileKey == null)
				return null;

			var file = await s3Adapter.GetObjectAsync(fileKey.ToString());
			if(file == null)
				return null;

			return file;	
		}

		public async Task<Guid> SaveContractFileAsync(IFormFile contractFile)
		{
			Guid fileKey = Guid.NewGuid();

			await s3Adapter.PutObjectAsync(contractFile, fileKey.ToString());

			return fileKey;
		}
	}
}
