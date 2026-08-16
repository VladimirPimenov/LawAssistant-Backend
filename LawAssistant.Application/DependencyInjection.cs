using Microsoft.Extensions.DependencyInjection;

using LawAssistant.Application.Contracts;
using LawAssistant.Application.Services;

namespace LawAssistant.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddScoped<INotificationService, SiteNotificationService>();

            services.AddScoped<IHashService, SHA256HashService>();
            services.AddScoped<ITokenProvider, JwtTokenProvider>();
            services.AddScoped<IAuthentificationService, AuthentificationService>();

            services.AddScoped<IContractService, ContractService>();
            services.AddScoped<IContractFileService, ContractFileService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<ILawyerService, LawyerService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IComparisonService, ComparisonService>();

            services.AddScoped<IDocumentParser, WordDocumentParser>();
        }
    }
    
}