using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
    /// <summary>
    /// Сервис для работы с данными юристов.
    /// Инкапсулирует логику работы с аккаунтами юристов
    /// </summary>
    internal class LawyerService(
        IAccountRepository accountRepository,
        IContractService contractService,
        IReportService reportService) 
        : ILawyerService
    {
        /// <summary>
        /// Получить список юристов
        /// </summary>
        /// <returns>Список юристов</returns>
        public async Task<List<AccountDto>> GetLawyersListAsync()
        {
            var lawyers = await accountRepository.GetAllAccountsAsync();

			var dtos = lawyers
				.Select(l => l.ConvertToDto())
				.ToList();
			return dtos;
        }
    
        /// <summary>
        /// Получает список договоров юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список договоров</returns>
        public async Task<List<ContractDto>> GetLawyerContractsAsync(int lawyerId)
        {
            return await contractService.GetLawyerContractsInfoAsync(lawyerId);
        }

        /// <summary>
        /// Получить список отчётов юриста
        /// </summary>
        /// <param name="lawyerId">Идентификатор юриста</param>
        /// <returns>Список отчётов</returns>
        public async Task<List<ReportDto>> GetLawyerReportsAsync(int lawyerId)
        {
            return await reportService.GetLawyerReportsAsync(lawyerId);
        }
    }
}