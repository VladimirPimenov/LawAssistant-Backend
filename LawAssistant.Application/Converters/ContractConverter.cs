using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;

namespace LawAssistant.Application.Converters
{
    public static class ContractConverter
    {
        public static ContractDto ConvertToDto(this CollectiveContract contract, List<LawyerDto> authors)
        {
            return new ContractDto
            {
                ContractId = contract.ContractId,
                Title = contract.Title,
                CreatedDate = contract.CreatedDate,
                FileKey = contract.FileKey,
                Authors = authors
            };
        }
    }
}
