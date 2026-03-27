namespace LawAssistant.Application.Models
{
    public record ContractDto
    {
		public int ContractId { get; set; }

		public string Title { get; set; }

		public DateTime CreatedDate { get; set; }
	}
}
