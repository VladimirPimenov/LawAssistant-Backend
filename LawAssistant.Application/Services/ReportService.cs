using LawAssistant.Application.Contracts;
using LawAssistant.Application.Converters;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;
using System.Security.AccessControl;

namespace LawAssistant.Application.Services
{
	public class ReportService(
		IReportRepository reportRepository,
		ILawDocumentsRepository lawDocumentsRepository,
		IComparisonRepository comparisonRepository,
		IContractService contractService) 
		: IReportService
	{
		public async Task<ComparisonReport> GetReportAsync(int reportId)
		{
			var report = await reportRepository.GetReportAsync(reportId);
			if (report == null)
				return null;

			report.ComparisonResults = await reportRepository.GetReportResultsAsync(report);

			return report;
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

		public async Task<ComparisonReport> CreateReportAsync(int contractId)
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

			createdReport.ComparisonResults = new List<ComparisonResult>();

			foreach(var paragraph in contract.ContractParagraphs)
			{
				foreach(var lawAct in lawActs)
				{
					var actComparisonResults = await CompareParagraphWithAct(paragraph, lawAct);
					var bestResult = GetMostMatchedResult(actComparisonResults);

					foreach(var result in actComparisonResults)
					{
						if(result.ResultId != bestResult.ResultId)
							await comparisonRepository.RemoveComparisonResultAsync(result);
					}

					createdReport.ComparisonResults.Add(bestResult);
					await reportRepository.AddResultToReportAsync(bestResult, createdReport);
				}
			}
			return createdReport;
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

		private ComparisonResult GetMostMatchedResult(List<ComparisonResult> results)
		{
			var maxMatchValue = results.Max(r => r.MatchValue);
			var bestResult = results.FirstOrDefault(r => r.MatchValue == maxMatchValue);

			return bestResult;
		}
	}
}
