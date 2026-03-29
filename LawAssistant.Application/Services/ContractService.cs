using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using LawAssistant.Application.Models;

namespace LawAssistant.Application.Services
{
    public class ContractService(
        IContractRepository contractRepository,
        IFileService fileService,
        IDocumentParser documentParser)
        : IContractService
    {
        public async Task<CollectiveContract> GetContractAsync(int contractId)
        {
            return await contractRepository.GetCollectiveContractAsync(contractId);
        }

        public async Task<List<ContractDto>> GetLawyerContractsInfoAsync(int lawyerId)
        {
            var contracts = await contractRepository.GetLawyerContractsAsync(lawyerId);

            var contractsInfo = contracts
                .Select(c => new ContractDto
                {
                    ContractId = c.ContractId,
                    Title = c.Title,
                    CreatedDate = c.CreatedDate
                }).ToList();

            return contractsInfo;
        }

        public async Task<CollectiveContract> CreateContractAsync(CreateContractRequest contractRequest)
        {
            var contract = new CollectiveContract
            {
                Title = contractRequest.Title,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                ContractParagraphs = new List<ContractParagraph>()
            };
            var createdContract = await contractRepository.CreateCollectiveContractAsync(contract);

            if (createdContract == null)
                return null;

            string documentKey = await fileService.LoadFileToServer(contractRequest.ContractFile);

            var paragraphs = documentParser.ParseDocumentIntoParagraphs(contractRequest.ContractFile);

            foreach (var paragraphText in paragraphs)
            {
                var paragraph = new ContractParagraph
                {
                    Text = paragraphText
                };
                createdContract.ContractParagraphs.Add(paragraph);
            }	

            createdContract = await contractRepository.UpdateContractAsync(createdContract);

            return createdContract;
        }

		public async Task<CollectiveContract> UpdateContractAsync(ContractDto contractDto)
		{
            var contract = await contractRepository.GetCollectiveContractAsync(contractDto.ContractId);

            if(contract == null)
                return null;

			var updatedContract = await contractRepository.UpdateContractAsync(contract);

            return updatedContract;
		}

		public async Task<int?> RemoveContractAsync(int contractId)
        {
            var contract = await contractRepository.GetCollectiveContractAsync(contractId);

            if (contract == null)
                return null;

            int? removedContractId = await contractRepository.RemoveContractAsync(contract);
            return removedContractId;
        }
    }
}
