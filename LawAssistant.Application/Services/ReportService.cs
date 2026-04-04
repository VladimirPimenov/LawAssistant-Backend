using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	public class ReportService(
		IReportRepository reportRepository,
		ILawDocumentsRepository lawDocumentsRepository,
		IComparisonRepository comparisonRepository,
		IContractService contractService) 
		: IReportService
	{
		public async Task<ComparisonReport> CreateComparisonReportAsync(int contractId)
		{
			var contract = await contractService.GetContractAsync(contractId);
			if (contract == null)
				return null;

			var lawActs = await lawDocumentsRepository.GetAllActsAsync();

			var report = new ComparisonReport
			{
				ReportedDate = DateTime.Now.ToUniversalTime()
			};

			var createdReport = await reportRepository.CreateReportAsync(report);
			if (createdReport == null)
				return null;

			createdReport.ComparisonResults = new List<ComparisonResult>();

			foreach(var paragraph in contract.ContractParagraphs)
			{
				foreach(var lawAct in lawActs)
				{
					foreach(var acrticle in lawAct.Articles)
					{
						int comparisonResultId = await comparisonRepository.CompareParagraphWithArticle(paragraph, acrticle);
						var result = await comparisonRepository.GetComparisonResultAsync(comparisonResultId);

						report.ComparisonResults.Add(result);
						await reportRepository.AddResultToReportAsync(result, report);
					}
				}
			}
			return report;
		}

		public async Task<ComparisonReport> GetReportAsync(int reportId)
		{
			var report = await reportRepository.GetReportAsync(reportId);
			if (report == null)
				return null;

			report.ComparisonResults = await reportRepository.GetReportResultsAsync(report);

			return report;
		}

		public Task<int> RemoveComparisonReportAsync(int reportId)
		{
			throw new NotImplementedException();
		}
	}
}
