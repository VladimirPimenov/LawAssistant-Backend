using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Models
{
	internal static class ReportConverter
    {
        public static ReportDto ConvertToDto(this ComparisonReport report, ContractDto contract)
        {
            return new ReportDto
            {
                ReportId = report.ReportId,
                ReportedDate = report.ReportedDate,
                Contract = contract
            };
        }

        public static async Task<ReportDetail> CreateDetailedReport(
            ComparisonReport report,
            List<ComparisonResult> comparisonResults,
            ILawDocumentsRepository articlesRepository)
        {
            var reportResults = await GetResultsForReport(comparisonResults, articlesRepository);
        
            var reportWithResults = new ReportDetail
            {
                ReportId = report.ReportId,
                ReportedDate = report.ReportedDate,
                ContractId = report.ContractId,
                Results = reportResults
                    .OrderBy(rr => rr.Paragraph.ParagraphId)
                    .ToList()
            };

            return reportWithResults;
        }
        
        private static async Task<List<ParagraphMatches>> GetResultsForReport(
            List<ComparisonResult> results,
            ILawDocumentsRepository lawDocumentsRepository)
		{
			var paragraphsMatches = new List<ParagraphMatches>();
			var paragraphs = results
				.Select(rr => rr.ContractParagraph)
				.Distinct()
				.OrderBy(p => p.ParagraphId)
				.ToList();

			foreach(var paragraph in paragraphs)
			{
				var paragraphCompareResults = new List<ArticleMatch>();

				var paragraphResults = results
					.Where(rr => rr.ParagraphId == paragraph.ParagraphId)
					.ToList();

				foreach(var paragraphResult in paragraphResults)
				{
					var article = await lawDocumentsRepository.GetArticleAsync(paragraphResult.ArticleId);
					var act = await lawDocumentsRepository.GetActAsync(article.ActId);
				
					var articleMatch = new ArticleMatch
					{
						ResultId = paragraphResult.ResultId,
						Text = paragraphResult.Text,
						MatchValue = paragraphResult.MatchValue,
						Article = GetArticleForReport(article, act)
					};

					paragraphCompareResults.Add(articleMatch);
				}

				var reportParagraph = new ParagraphMatches
				{
					Paragraph = paragraph,
					ComparisonResults = paragraphCompareResults
						.OrderByDescending(cr => cr.MatchValue)
						.ToList()
				};
				paragraphsMatches.Add(reportParagraph);
			}

			return paragraphsMatches;
		}
        
        private static ArticleWithAct GetArticleForReport(ActArticle article, LawAct act)
        {
            return new ArticleWithAct
            {
                ActId = act.ActId,
                ActTitle = act.Title,
                Article = article
            };
        }
    }
}
