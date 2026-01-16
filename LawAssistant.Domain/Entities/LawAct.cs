namespace LawAssistant.Domain.Entities
{
	public class LawAct
	{
		public int ActId { get; set; }

		public string Title { get; set; }

		public DateOnly AdoptedDate { get; set; }
	}
}
