using Microsoft.Extensions.DependencyInjection;

using LawAssistant.Domain.Repositories;

using LawAssistant.Application.Contracts;

using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;
using LawAssistant.Infrastructure.S3;

namespace LawAssistant.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureLayer(this IServiceCollection services)
        {
            services.AddScoped<ILawyerRepository, LawyerRepository>();
            services.AddScoped<IContractRepository, ContractRepository>();
            services.AddScoped<ILawDocumentsRepository, LawDocumentsRepository>();
            services.AddScoped<IReportRepository, ReportRepository>();
            services.AddScoped<IComparisonRepository, ComparisonRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            
            services.AddScoped<IS3Adapter, S3MockService>();
            services.AddScoped<ISemanticModuleApiClient, SemanticModuleClient>();
        }
    }
    
}