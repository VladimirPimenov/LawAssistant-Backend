using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Entities;
using LawAssistant.Infrastructure.RepositoryImplementation.Models;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;

namespace LawAssistant.Infrastructure.RepositoryImplementation
{
	public class PostgreSqlDbContext: DbContext
	{
		public DbSet<Lawyer> Lawyer { get; set; }

		public DbSet<LawAct> LawAct { get; set; }
		public DbSet<ActArticle> ActArticle { get; set; }

		public DbSet<CollectiveContract> CollectiveContract { get; set; }
		public DbSet<ContractParagraph> ContractParagraph { get; set; }
		public DbSet<LawyerContract> LawyerContract { get; set; }

		public DbSet<ComparisonReport>  ComparisonReport { get; set; }
		public DbSet<ComparisonResult> ComparisonResult { get; set; }
		public DbSet<ReportResult> ReportResult { get; set; }
		public DbSet<LawyerReport> LawyerReport { get; set; }

		public PostgreSqlDbContext(DbContextOptions<PostgreSqlDbContext> options) : base(options) { }

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Lawyer>().HasKey(l => l.LawyerId);
			modelBuilder.Entity<LawAct>().HasKey(la => la.ActId);
			modelBuilder.Entity<ActArticle>().HasKey(a => a.ArticleId);
			modelBuilder.Entity<CollectiveContract>().HasKey(c => c.ContractId);
			modelBuilder.Entity<ContractParagraph>().HasKey(ph => ph.ParagraphId);
			modelBuilder.Entity<ComparisonResult>().HasKey(cr => cr.ResultId);
			modelBuilder.Entity<ComparisonReport>().HasKey(cre => cre.ReportId);
			modelBuilder.Entity<ReportResult>().HasKey(rr => rr.ReportResultId);
			modelBuilder.Entity<LawyerReport>().HasKey(lr => lr.LawyerReportId);

			modelBuilder.Entity<CollectiveContract>()
				.HasMany(c => c.ContractParagraphs)
				.WithOne()
				.HasForeignKey(c => c.ContractId);
			
			modelBuilder.Entity<LawAct>()
				.HasMany(act => act.Articles)
				.WithOne()
				.HasForeignKey(a => a.ActId);

			modelBuilder.Entity<Lawyer>()
				.Property(l => l.HashedPassword)
				.HasColumnName("Password");

			modelBuilder.Entity<ComparisonResult>()
				.HasOne(r => r.ContractParagraph)
				.WithMany()
				.HasForeignKey(r => r.ParagraphId);
		}
	}
}
