namespace LawAssistant.Domain.Entities
{
    /// <summary>
    /// Отчёт о сопоставлении коллективного договора с законодательными актами
    /// </summary>
    public class ComparisonReport
    {
        /// <summary>
        /// Идентификатор отчёта
        /// </summary>
        public int ReportId { get; set; }

        /// <summary>
        /// Дата создания отчёта
        /// </summary>
        public DateTime ReportedDate { get; set; }

        /// <summary>
        /// Идентификатор договора, для которого составлен отчёт
        /// </summary>
        public int ContractId { get; set; }
    }
}
