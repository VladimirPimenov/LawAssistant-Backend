using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using LawAssistant.Infrastructure.Settings;

using LawAssistant.Application.Contracts;

using LawAssistant.Domain.Repositories;

using LawAssistant.Infrastructure.RepositoryImplementation;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;
using LawAssistant.Infrastructure.S3;

namespace LawAssistant.Infrastructure
{
    public static class DependencyInjection
    {
        public static void AddInfrastructureLayer(
            this IServiceCollection services, 
            IConfiguration config)
        {
			services.AddDbContext<PostgreSqlDbContext>(options =>
	            options.UseNpgsql(
		            config
		            .GetSection(nameof(DbConfiguration))
		            .Get<DbConfiguration>().PostreSqlConnectionString));

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