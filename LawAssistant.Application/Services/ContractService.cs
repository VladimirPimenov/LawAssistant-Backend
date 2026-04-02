using Microsoft.AspNetCore.Http;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using LawAssistant.Application.Models;

namespace LawAssistant.Application.Services
{
    public class ContractService(
        IContractRepository contractRepository,
        ILawyerRepository lawyerRepository,
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
            var contractsInfo = new List<ContractDto>();

            foreach(var contract in contracts)
            {
                var authors = await contractRepository.GetContractAuthorsAsync(contract);
                var authorsDto = authors.
                    Select(a => new LawyerDto
                    {
                        FirstName = a.FirstName,
                        LastName = a.LastName,
                        Email = a.Email
                    }).ToList();

                contractsInfo.Add(new ContractDto
                {
                    ContractId = contract.ContractId,
                    Title = contract.Title,
                    CreatedDate = contract.CreatedDate,
                    Authors = authorsDto
                });
            }

            return contractsInfo;
        }

        public async Task<CollectiveContract> CreateContractAsync(CreateContractRequest contractRequest)
        {
            var contractAuthors = await GetContractAuthorsAsync(contractRequest);

            if (contractAuthors == null)
                return null;

			var contract = new CollectiveContract
			{
				Title = contractRequest.Title,
				CreatedDate = DateTime.Now.ToUniversalTime(),
				ContractParagraphs = new List<ContractParagraph>()
			};

			var createdContract = await contractRepository.CreateCollectiveContractAsync(contract);

            if (createdContract == null)
                return null;

			await AddAuthorsToContractAsync(contractAuthors, createdContract);

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

            var authors = await contractRepository.GetContractAuthorsAsync(contract);
            await RemoveAuthorsFromContractAsync(authors, contract);

            int? removedContractId = await contractRepository.RemoveContractAsync(contract);
            return removedContractId;
        }

        private async Task<List<Lawyer>> GetContractAuthorsAsync(CreateContractRequest contractRequest)
        {
            var authorsFindTasks = contractRequest.AuthorsId.Select(lawyerRepository.GetLawyerAsync);
            var authors = await Task.WhenAll(authorsFindTasks);

            return authors.ToList();
        }

        private async Task AddAuthorsToContractAsync(List<Lawyer> authors, CollectiveContract contract)
        {
            var tasks = new List<Task>();

            foreach (var author in authors)
            {
                tasks.Add(contractRepository.AddAuthorToContractAsync(author.LawyerId, contract.ContractId));
            }
            await Task.WhenAll(tasks);
        }

		private async Task RemoveAuthorsFromContractAsync(List<Lawyer> authors, CollectiveContract contract)
		{
			var tasks = new List<Task>();

			foreach (var author in authors)
			{
				tasks.Add(contractRepository.RemoveAuthorFromContractAsync(author.LawyerId, contract.ContractId));
			}
			await Task.WhenAll(tasks);
		}
	}
}
