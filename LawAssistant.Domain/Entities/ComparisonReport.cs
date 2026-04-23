namespace LawAssistant.Domain.Entities
{
    public class ComparisonReport
    {
        public int ReportId { get; set; }

        public DateTime ReportedDate { get; set; }

        public int ContractId { get; set; }
    }
}
