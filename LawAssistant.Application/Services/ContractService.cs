using LawAssistant.Application.Contracts;

using LawAssistant.Application.Converters;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
    public class ContractService(
        IContractRepository contractRepository,
        ILawyerService lawyerService,
        IContractFileService fileService,
        IDocumentParser documentParser,
        INotificationService notificationService)
        : IContractService
    {
        public async Task<ContractDto> GetContractAsync(int contractId)
        {
            var contract = await contractRepository.GetContractAsync(contractId);
            if (contract == null)
                return null;

            var contractAuthors = await contractRepository.GetContractAuthorsAsync(contract);
            var authorsDto = contractAuthors
                .Select(a => a.ConvertToDto())
                .ToList();
            var contractDto = contract.ConvertToDto(authorsDto);

            return contractDto;
        }

        public async Task<CollectiveContract> GetContractWithParagraphsAsync(int contractId)
        {
            return await contractRepository.GetContractWithParagraphsAsync(contractId);
        }

        public async Task<List<ContractDto>> GetLawyerContractsInfoAsync(int lawyerId)
        {
            var contracts = await contractRepository.GetLawyerContractsAsync(lawyerId);
            var contractsInfo = new List<ContractDto>();

            foreach(var contract in contracts)
            {
                var authors = await contractRepository.GetContractAuthorsAsync(contract);
                var authorsDto = authors
                    .Select(a => a.ConvertToDto())
                    .ToList();
                var contractDto = contract.ConvertToDto(authorsDto);

                contractsInfo.Add(contractDto);
            }

            return contractsInfo;
        }

        public async Task<CollectiveContract> CreateContractAsync(CreateContractRequest contractRequest)
        {
            var contractAuthors = await GetContractAuthorsFromRequestAsync(contractRequest);

            if (contractAuthors == null)
                return null;

			var contract = new CollectiveContract
			{
				Title = contractRequest.Title,
				CreatedDate = DateTime.Now.ToUniversalTime(),
				ContractParagraphs = new List<ContractParagraph>()
			};

			var createdContract = await contractRepository.CreateContractAsync(contract);
            if (createdContract == null)
                return null;

			await AddAuthorsToContractAsync(contractAuthors, createdContract);

            Guid fileKey = await fileService.SaveContractFileAsync(contractRequest.ContractFile);
            createdContract.FileKey = fileKey;

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

            await CreateAuthorsNotificationAsync(createdContract, contractAuthors);

            return createdContract;
        }

		public async Task<CollectiveContract> UpdateContractAsync(ContractDto contractDto)
		{
            var dbContract = await contractRepository.GetContractAsync(contractDto.ContractId);

            if(dbContract == null)
                return null;

            dbContract.Title = contractDto.Title;
            dbContract.CreatedDate = dbContract.CreatedDate.ToUniversalTime();

			var updatedContract = await contractRepository.UpdateContractAsync(dbContract);

			var dbContractAuthors = await contractRepository.GetContractAuthorsAsync(dbContract);
			var addedAuthors = await GetAddedAuthorsAsync(contractDto, dbContractAuthors);
			var removedAuthors = await GetRemovedAuthorsAsync(contractDto, dbContractAuthors);

            await AddAuthorsToContractAsync(addedAuthors, updatedContract);
            await RemoveAuthorsFromContractAsync(removedAuthors, updatedContract);

			return updatedContract;
		}

		public async Task<int?> RemoveContractAsync(int contractId)
        {
            var contract = await contractRepository.GetContractWithParagraphsAsync(contractId);
            if (contract == null)
                return null;

            var authors = await contractRepository.GetContractAuthorsAsync(contract);
            await RemoveAuthorsFromContractAsync(authors, contract);

            int? removedContractId = await contractRepository.RemoveContractAsync(contract);
            return removedContractId;
        }

        private async Task<List<Lawyer>> GetContractAuthorsFromRequestAsync(CreateContractRequest contractRequest)
        {
            var authors = new List<Lawyer>();

            foreach(var authorId in contractRequest.AuthorsId)
            {
                var author = await lawyerService.GetLawyerAsync(authorId);
                authors.Add(author);
            }
            return authors;
        }

        private async Task AddAuthorsToContractAsync(List<Lawyer> authors, CollectiveContract contract)
        {
            foreach (var author in authors)
            {
                await contractRepository.AddAuthorToContractAsync(author.LawyerId, contract.ContractId);
            }
            await Task.CompletedTask;
        }

		private async Task RemoveAuthorsFromContractAsync(List<Lawyer> authors, CollectiveContract contract)
		{
			foreach (var author in authors)
			{
				await contractRepository.RemoveAuthorFromContractAsync(author.LawyerId, contract.ContractId);
			}
            await Task.CompletedTask;
		}

        private async Task<List<Lawyer>> GetAddedAuthorsAsync(ContractDto updatedContract, List<Lawyer> dbAuthors)
        {
            var dbContractAuthorsId = dbAuthors.Select(a => a.LawyerId).ToList();

            var updatedContactAuthorsId = updatedContract.Authors.Select(a => a.LawyerId).ToList();

            var addedAuthorsId = updatedContactAuthorsId.Except(dbContractAuthorsId);

            var addedAuthors = new List<Lawyer>();

            foreach(var authorId in addedAuthorsId)
            {
                var author = await lawyerService.GetLawyerAsync(authorId);
                addedAuthors.Add(author);
            }
            return addedAuthors;
        }

		private async Task<List<Lawyer>> GetRemovedAuthorsAsync(ContractDto updatedContract, List<Lawyer> dbAuthors)
		{
			var dbContractAuthorsId = dbAuthors.Select(a => a.LawyerId).ToList();

			var updatedContactAuthorsId = updatedContract.Authors.Select(a => a.LawyerId).ToList();

			var removedAuthorsId = dbContractAuthorsId.Except(updatedContactAuthorsId);

			var removedAuthors = new List<Lawyer>();

			foreach (var authorId in removedAuthorsId)
			{
				var author = await lawyerService.GetLawyerAsync(authorId);
				removedAuthors.Add(author);
			}
			return removedAuthors;
		}
    
        private async Task CreateAuthorsNotificationAsync(CollectiveContract contract, List<Lawyer> lawyers)
        {
            string notificationText = $"Загружен документ «{contract.Title}»";

            foreach(var lawyer in lawyers)
            {
                await notificationService.CreateNotificationAsync(notificationText, lawyer.LawyerId);
            }
        }
    }
}