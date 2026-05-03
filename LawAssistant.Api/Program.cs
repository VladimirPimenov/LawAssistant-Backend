using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi;

using LawAssistant.Domain.Repositories;
using LawAssistant.Application.Contracts;
using LawAssistant.Application.Services;

using LawAssistant.Infrastructure;
using LawAssistant.Infrastructure.FileStorage;
using LawAssistant.Infrastructure.RepositoryImplementation;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;

using LawAssistant.Api.Extensions;
using LawAssistant.Api.Settings;
using LawAssistant.Application.Services.Authentification;
using LawAssistant.Application.Contracts.S3;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo
	{
		Title = "Web API LawAssistant",
		Description = "REST API сервиса анализа коллективных договоров LawAssistant",
		Version = "v1",
	});
});

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("http://localhost:3000");
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
	});
});

builder.Services.AddDbContext<PostgreSqlDbContext>(options =>
	options.UseNpgsql(
		builder.Configuration
		.GetSection(nameof(DbConfiguration))
		.Get<DbConfiguration>().PostreSqlConnectionString));

builder.Services.AddHttpClient();

builder.Services.AddScoped<ILawyerRepository, LawyerRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ILawDocumentsRepository, LawDocumentsRepository>();
builder.Services.AddScoped<IReportRepository, ReportRepository>();
builder.Services.AddScoped<IComparisonRepository, ComparisonRepository>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IS3Adapter, S3MockService>();
builder.Services.AddScoped<ISemanticModuleApiClient, SemanticModuleClient>();

builder.Services.AddScoped<INotificationService, SiteNotificationService>();

builder.Services.AddScoped<IHashService, SHA256HashService>();
builder.Services.AddScoped<ITokenProvider, JwtTokenProvider>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();

builder.Services.AddScoped<IContractService, ContractService>();
builder.Services.AddScoped<IContractFileService, ContractFileService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<ILawyerService, LawyerService>();
builder.Services.AddScoped<IComparisonService, ComparisonService>();

builder.Services.AddScoped<IDocumentParser, WordDocumentParser>();

builder.Services.AddJwtAuthentification(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();