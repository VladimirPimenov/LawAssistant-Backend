using LawAssistant.Application.Models;

namespace LawAssistant.Application.Contracts
{
    public interface ILawyerService
    {
        public Task<List<AccountDto>> GetLawyersListAsync();
    
        public Task<List<ContractDto>> GetLawyerContractsAsync(int lawyerId);
        
        public Task<List<ReportDto>> GetLawyerReportsAsync(int lawyerId);
    }
}