using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;

using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	internal class ContractService(
        IContractRepository contractRepository,
        IAccountService accountService,
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
				ModifiedDate = DateTime.Now.ToUniversalTime(),
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
            dbContract.ModifiedDate = DateTime.Now.ToUniversalTime();

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
            
            await fileService.DeleteContractFileAsync(contract.ContractId);

            int? removedContractId = await contractRepository.RemoveContractAsync(contract);
            return removedContractId;
        }

        private async Task<List<Account>> GetContractAuthorsFromRequestAsync(CreateContractRequest contractRequest)
        {
            var authors = new List<Account>();

            foreach(var authorId in contractRequest.AuthorsId)
            {
                var author = await accountService.GetAccountAsync(authorId);
                authors.Add(author);
            }
            return authors;
        }

        private async Task AddAuthorsToContractAsync(List<Account> authors, CollectiveContract contract)
        {
            foreach (var author in authors)
            {
                await contractRepository.AddAuthorToContractAsync(author.AccountId, contract.ContractId);
            }
            await Task.CompletedTask;
        }

		private async Task RemoveAuthorsFromContractAsync(List<Account> authors, CollectiveContract contract)
		{
			foreach (var author in authors)
			{
				await contractRepository.RemoveAuthorFromContractAsync(author.AccountId, contract.ContractId);
			}
            await Task.CompletedTask;
		}

        private async Task<List<Account>> GetAddedAuthorsAsync(ContractDto updatedContract, List<Account> dbAuthors)
        {
            var dbContractAuthorsId = dbAuthors.Select(a => a.AccountId).ToList();

            var updatedContactAuthorsId = updatedContract.Authors.Select(a => a.AccountId).ToList();

            var addedAuthorsId = updatedContactAuthorsId.Except(dbContractAuthorsId);

            var addedAuthors = new List<Account>();

            foreach(var authorId in addedAuthorsId)
            {
                var author = await accountService.GetAccountAsync(authorId);
                addedAuthors.Add(author);
            }
            return addedAuthors;
        }

		private async Task<List<Account>> GetRemovedAuthorsAsync(ContractDto updatedContract, List<Account> dbAuthors)
		{
			var dbContractAuthorsId = dbAuthors.Select(a => a.AccountId).ToList();

			var updatedContactAuthorsId = updatedContract.Authors.Select(a => a.AccountId).ToList();

			var removedAuthorsId = dbContractAuthorsId.Except(updatedContactAuthorsId);

			var removedAuthors = new List<Account>();

			foreach (var authorId in removedAuthorsId)
			{
				var author = await accountService.GetAccountAsync(authorId);
				removedAuthors.Add(author);
			}
			return removedAuthors;
		}
    
        private async Task CreateAuthorsNotificationAsync(CollectiveContract contract, List<Account> authors)
        {
            string notificationText = $"Загружен документ «{contract.Title}»";

            foreach(var author in authors)
            {
                await notificationService.CreateNotificationAsync(notificationText, author.AccountId);
            }
        }
    }
}