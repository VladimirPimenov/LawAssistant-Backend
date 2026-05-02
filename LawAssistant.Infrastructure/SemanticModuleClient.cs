using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.Configuration;

using LawAssistant.Application.Contracts;
using LawAssistant.Domain.Entities;

using LawAssistant.Infrastructure.Settings;

namespace LawAssistant.Infrastructure
{
	public class SemanticModuleClient(
		IConfiguration config,
		IHttpClientFactory httpClientFactory
		) : ISemanticModuleApiClient
	{
		private readonly SemanticModuleConfiguration semanticModuleConfig = 
			config.GetSection(nameof(SemanticModuleConfiguration)).Get<SemanticModuleConfiguration>();

		public async Task<ComparisonResult> CompareWithEmbeddingAsync(ComparisonResult comparisonResult)
		{
			string requestString = semanticModuleConfig.URL + "/compare/compare-with-embedding";

			var content = JsonContent.Create(comparisonResult);

			HttpClient httpClient = httpClientFactory.CreateClient();

			using var response = await httpClient.PutAsync(requestString, content);

			if (response.StatusCode != HttpStatusCode.OK)
				return null;

			var semanticComparisonResult = await response.Content.ReadFromJsonAsync<ComparisonResult>();
			return semanticComparisonResult;
		}
	}
}
