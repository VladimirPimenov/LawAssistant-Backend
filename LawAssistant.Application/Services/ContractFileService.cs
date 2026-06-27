using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	internal class ContractFileService(
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
		
		public async Task DeleteContractFileAsync(int contractId)
        {
			var contract = await contractRepository.GetContractAsync(contractId);

			var fileKey = contract.FileKey;
            await s3Adapter.DeleteObjectAsync(fileKey.ToString());
        }
	}
}
