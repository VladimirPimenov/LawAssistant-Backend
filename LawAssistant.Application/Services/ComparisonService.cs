using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;
using LawAssistant.Domain.Repositories;

namespace LawAssistant.Application.Services
{
    internal class ComparisonService(
        IComparisonRepository comparisonRepository,
        ILawDocumentsRepository lawDocsRepository,
        ISemanticModuleApiClient semanticModule,
        ISyntaxModuleApiClient syntaxModule) 
        : IComparisonService
    {
        private readonly int bestResultsCount = 5;
        private readonly double minMatchValue = 0.001;

        public async Task<ComparisonResult> GetComparisonResultAsync(int resultId) =>
            await comparisonRepository.GetComparisonResultAsync(resultId);

        public async Task<int?> RemoveComparisonResultAsync(int resultId)
        {
            var result = await comparisonRepository.GetComparisonResultAsync(resultId);
            if (result == null)
                return null;

            var removedResultId = await comparisonRepository.RemoveComparisonResultAsync(result);

            return removedResultId;
        }

        public async Task<List<ComparisonResult>> MakeSyntacticComparisonAsync(List<ContractParagraph> paragraphs)
        {
            var lawActs = await lawDocsRepository.GetAllActsAsync();

            var results = new List<ComparisonResult>();

            foreach (var paragraph in paragraphs)
            {
                foreach (var lawAct in lawActs)
                {
                    var actComparisonResults = await CompareParagraphWithAct(paragraph, lawAct);
                    var bestResults = GetMostMatchedResults(actComparisonResults, bestResultsCount);
                    await RemoveResultsBesidesBest(actComparisonResults, bestResults);

                    foreach (var result in bestResults)
                    {
                        results.Add(result);
                    }
                }
            }
            return results;
        }

        public async Task MakeSemanticComparisonAsync(List<ComparisonResult> syntacticComparisonResults)
        {
            foreach (var result in syntacticComparisonResults)
            {
                var semanticResult = await semanticModule.MakeSemanticComparisonAsync(result);

                if (semanticResult == null)
                    continue;

                result.MatchValue = semanticResult.MatchValue;

                await comparisonRepository.UpdateComparisonResultAsync(result);
            }
        }

        private async Task<List<ComparisonResult>> CompareParagraphWithAct(ContractParagraph paragraph, LawAct act)
        {
            var comparisonResults = new List<ComparisonResult>();

            foreach (var acrticle in act.Articles)
            {
                int comparisonResultId = await syntaxModule.MakeSyntaxComparisonAsync(paragraph, acrticle);
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

            foreach (var result in allResults)
            {
                if (!bestResultsId.Contains(result.ResultId))
                    await comparisonRepository.RemoveComparisonResultAsync(result);
            }
        }
    }
}
