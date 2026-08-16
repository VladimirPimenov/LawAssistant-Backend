namespace LawAssistant.Application.Models
{
    public record ContractDto
    {
        public int ContractId { get; init; }

        public string Title { get; init; }

        public DateTime CreatedDate { get; init; }
        
        public DateTime ModifiedDate { get; init; }

        public Guid? FileKey { get; init;  }

        public List<AccountDto> Authors { get; init; }
    }
}
