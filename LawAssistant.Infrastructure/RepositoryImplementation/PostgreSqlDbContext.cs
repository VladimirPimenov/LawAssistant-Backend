using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;

namespace LawAssistant.Infrastructure.RepositoryImplementation
{
	public class PostgreSqlDbContext: DbContext
	{
		public DbSet<Lawyer> Lawyer { get; set; }

		public DbSet<LawAct> LawAct { get; set; }
		public DbSet<ActArticle> ActArticle { get; set; }

		public DbSet<CollectiveContract> CollectiveContract { get; set; }
		public DbSet<ContractParagraph> ContractParagraph { get; set; }

		public DbSet<ComparisonReport>  ComparisonReport { get; set; }
		public DbSet<ComparisonResult> ComparisonResult { get; set; }
	}
}
