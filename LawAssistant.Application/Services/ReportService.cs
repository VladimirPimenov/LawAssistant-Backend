using LawAssistant.Application.Contracts;
using LawAssistant.Application.Models;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
	internal class ReportService(
		IReportRepository reportRepository,
		IContractService contractService,
		IComparisonService comparisonService,
		ILawDocumentsRepository lawDocumentsRepository,
		INotificationService notificationService) 
		: IReportService
	{
		public async Task<ReportDetail> GetReportAsync(int reportId)
		{
			var report = await reportRepository.GetReportAsync(reportId);
			if (report == null)
				return null;

			var reportWithResults = await GetDetaliedReport(report);
			
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

		public async Task<ReportDetail> CreateReportAsync(int contractId)
		{
			var contract = await contractService.GetContractWithParagraphsAsync(contractId);
			if (contract == null)
				return null;

			var report = new ComparisonReport
			{
				ReportedDate = DateTime.Now.ToUniversalTime(),
				ContractId = contractId
			};

			var createdReport = await reportRepository.CreateReportAsync(report);
			if (createdReport == null)
				return null;

			await SetContractAuthorsToReport(contract, createdReport);

			var syntacticComparisonResults = await comparisonService.MakeSyntacticComparisonAsync(contract.ContractParagraphs);
			foreach(var result in syntacticComparisonResults)
			{
				await reportRepository.AddResultToReportAsync(result, createdReport);
			}

			await comparisonService.MakeSemanticComparisonAsync(syntacticComparisonResults);

			await CreateAuthorsNotificationAsync(createdReport);
			
			return await GetDetaliedReport(createdReport);
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
				await comparisonService.RemoveComparisonResultAsync(result.ResultId);
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

		private async Task<ReportDetail> GetDetaliedReport(ComparisonReport report)
		{
			var comparisonResults = await reportRepository.GetReportResultsAsync(report);
			return await ReportConverter.CreateDetailedReport(report, comparisonResults, lawDocumentsRepository);
		}

		private async Task CreateAuthorsNotificationAsync(ComparisonReport report)
		{
			var reportedContract = await contractService.GetContractAsync(report.ContractId);

			string notificationText = $"Создан отчёт по документу «{reportedContract.Title}»";

			foreach (var lawyer in reportedContract.Authors)
			{
				await notificationService.CreateNotificationAsync(notificationText, lawyer.LawyerId);
			}
		}
	}
}
