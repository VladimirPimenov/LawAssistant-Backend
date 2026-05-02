using LawAssistant.Application.Contracts;
using LawAssistant.Application.Converters;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	public class ReportService(
		IReportRepository reportRepository,
		ILawDocumentsRepository lawDocumentsRepository,
		IComparisonRepository comparisonRepository,
		IContractService contractService,
		ISemanticModuleApiClient semanticModuleClient) 
		: IReportService
	{
		private readonly int bestResultsCount = 5;
		private readonly double minMatchValue = 0.001;

		public async Task<ReportWithResults> GetReportAsync(int reportId)
		{
			var report = await reportRepository.GetReportAsync(reportId);
			if (report == null)
				return null;

			var reportWithResults = await CreateReportForFrontendAsync(report);
			
			return reportWithResults;
		}

		public async Task<List<ReportDto>> GetContractReportsAsync(int contractId)
		{
			var contract = await contractService.GetContractAsync(contractId);
			if (contract == null)
				return null;

			var reports = await reportRepository.GetContractReportsAsync(contract.ContractId);
			var reportsDto = new List<ReportDto>();

			foreach (var report in reports)
			{
				var dto = report.ConvertToDto(contract);
				reportsDto.Add(dto);
			}
			return reportsDto;
		}

		public async Task<List<ReportDto>> GetLawyerReportsAsync(int lawyerId)
		{
			var reports = await reportRepository.GetLawyerReportsAsync(lawyerId);
			var reportsDto = new List<ReportDto>();

			foreach (var report in reports)
			{
				var contract = await contractService.GetContractAsync(report.ContractId);

				var dto = report.ConvertToDto(contract);
				reportsDto.Add(dto);
			}
			return reportsDto;
		}

		public async Task<ReportWithResults> CreateReportAsync(int contractId)
		{
			var contract = await contractService.GetContractWithParagraphsAsync(contractId);
			if (contract == null)
				return null;

			var lawActs = await lawDocumentsRepository.GetAllActsAsync();

			var report = new ComparisonReport
			{
				ReportedDate = DateTime.Now.ToUniversalTime(),
				ContractId = contractId
			};

			var createdReport = await reportRepository.CreateReportAsync(report);
			if (createdReport == null)
				return null;

			await SetContractAuthorsToReport(contract, createdReport);

			foreach(var paragraph in contract.ContractParagraphs)
			{
				foreach(var lawAct in lawActs)
				{
					var actComparisonResults = await CompareParagraphWithAct(paragraph, lawAct);
					var bestResults = GetMostMatchedResults(actComparisonResults, bestResultsCount);
					await RemoveResultsBesidesBest(actComparisonResults, bestResults);

					foreach(var result in bestResults)
					{
						await reportRepository.AddResultToReportAsync(result, createdReport);
					}
				}
			}
			var syntacticResults = await reportRepository.GetReportResultsAsync(createdReport);
			await MakeSemanticComparison(syntacticResults);

			var reportWithResults = await CreateReportForFrontendAsync(createdReport);
			return reportWithResults;
		}

		public async Task<int?> RemoveReportAsync(int reportId)
		{
			var report = await reportRepository.GetReportAsync(reportId);
			if (report == null)
				return null;

			var reportLawyers = await reportRepository.GetReportLawyersAsync(report);
			await RemoveAllLawyersFromReport(reportLawyers, report);

			var reportResults = await reportRepository.GetReportResultsAsync(report);

			var removedReportId = await reportRepository.RemoveReportAsync(report);

			foreach(var result in reportResults)
			{
				await comparisonRepository.RemoveComparisonResultAsync(result);
			}

			return removedReportId;
		}

		private async Task SetContractAuthorsToReport(CollectiveContract contract, ComparisonReport report)
		{
			var contractWithAuthors = await contractService.GetContractAsync(contract.ContractId);

			foreach(var author in contractWithAuthors.Authors)
			{
				var lawyer = new Lawyer
				{
					LawyerId = author.LawyerId,
					FirstName = author.FirstName,
					LastName = author.LastName,
					Email = author.Email
				};

				await reportRepository.AddReportToLawyerAsync(report, lawyer);
			}
		}

		private async Task RemoveAllLawyersFromReport(List<Lawyer> lawyers, ComparisonReport report)
		{
			foreach(var lawyer in lawyers)
			{
				await reportRepository.RemoveReportFromLawyerAsync(report, lawyer);
			}
			await Task.CompletedTask;
		}

		private async Task<List<ComparisonResult>> CompareParagraphWithAct(ContractParagraph paragraph, LawAct act)
		{
			var comparisonResults = new List<ComparisonResult>();

			foreach (var acrticle in act.Articles)
			{
				int comparisonResultId = await comparisonRepository.CompareParagraphWithArticle(paragraph, acrticle);
				var result = await comparisonRepository.GetComparisonResultAsync(comparisonResultId);

				comparisonResults.Add(result);
			}

			return comparisonResults;
		}

		private List<ComparisonResult> GetMostMatchedResults(List<ComparisonResult> results, int topCount)
		{
			var bestResults = results
				.Where(r => r.MatchValue >= minMatchValue)
				.OrderByDescending(r => r.MatchValue)
				.Take(topCount)
				.ToList();

			return bestResults;
		}
	
		private async Task RemoveResultsBesidesBest(List<ComparisonResult> allResults, List<ComparisonResult> bestResults)
		{
			var bestResultsId = bestResults
				.Select(r => r.ResultId)
				.ToList();

			foreach(var result in allResults)
			{
				if (!bestResultsId.Contains(result.ResultId))
					await comparisonRepository.RemoveComparisonResultAsync(result);
			}
		}
	
		private async Task<ReportWithResults> CreateReportForFrontendAsync(ComparisonReport report)
		{
			var comparisonResults = await reportRepository.GetReportResultsAsync(report);
			var paragraphsForReport = await GetParagraphsForReportAsync(comparisonResults);

			return report.CreateReportForView(paragraphsForReport);
		}

		private async Task<List<ReportParagraph>> GetParagraphsForReportAsync(List<ComparisonResult> results)
		{
			var paragraphsForReport = new List<ReportParagraph>();
			var paragraphs = results
				.Select(rr => rr.ContractParagraph)
				.Distinct()
				.OrderBy(p => p.ParagraphId)
				.ToList();

			foreach(var paragraph in paragraphs)
			{
				var resultsForParagraph = new List<ResultDto>();

				var paragraphResults = results
					.Where(rr => rr.ParagraphId == paragraph.ParagraphId)
					.ToList();

				foreach(var paragraphResult in paragraphResults)
				{
					var resultDto = new ResultDto
					{
						ResultId = paragraphResult.ResultId,
						Text = paragraphResult.Text,
						MatchValue = paragraphResult.MatchValue,
						Article = await lawDocumentsRepository.GetArticleAsync(paragraphResult.ArticleId)
					};

					resultsForParagraph.Add(resultDto);
				}

				var reportParagraph = new ReportParagraph
				{
					Paragraph = paragraph,
					ComparisonResults = resultsForParagraph
						.OrderByDescending(cr => cr.MatchValue)
						.ToList()
				};
				paragraphsForReport.Add(reportParagraph);
			}

			return paragraphsForReport;
		}
	
		private async Task MakeSemanticComparison(List<ComparisonResult> syntacticResults)
		{
			foreach(var result in syntacticResults)
			{
				var semanticResult = await semanticModuleClient.CompareWithEmbeddingAsync(result);

				if (semanticResult == null)
					continue;

				result.MatchValue = semanticResult.MatchValue;
				await comparisonRepository.UpdateComparisonResultAsync(result);
			}
		}
	}
}
