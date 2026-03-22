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
        public Task<CollectiveContract> GetContract(int contractId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CollectiveContract>> GetLawyerContractsAsync(int lawyerId)
        {
            throw new NotImplementedException();
        }

        public async Task<CollectiveContract> CreateContractAsync(ContractDto contractDto)
        {
            var contract = new CollectiveContract
            {
                Title = contractDto.Title,
                CreatedDate = DateTime.Now.ToUniversalTime(),
                ContractParagraphs = new List<ContractParagraph>()
            };
            var createdContract = await contractRepository.CreateCollectiveContractAsync(contract);

            if (createdContract == null)
                return null;

            string documentPath = await fileService.LoadFileToServer(contractDto.ContractFile);
            if (documentPath == null)
                return null;


            var paragraphs = documentParser.ParseDocumentIntoParagraphs(documentPath);

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


        public Task<int> RemoveContractAsync(int contractId)
        {
            throw new NotImplementedException();
        }

        public Task<CollectiveContract> UpdateContractAsync(CollectiveContract updatedContract)
        {
            throw new NotImplementedException();
        }
    }
}
