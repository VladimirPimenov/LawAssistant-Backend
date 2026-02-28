namespace LawAssistant.Domain.Entities
{
	public class LawAct
	{
		public int ActId { get; set; }

		public string Title { get; set; }

		public DateTime AdoptedDate { get; set; }

		public List<ActArticle> Articles { get; set; }
	}
}
