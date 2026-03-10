using Microsoft.EntityFrameworkCore;

using LawAssistant.Domain.Repositories;
using LawAssistant.Application.Contracts;
using LawAssistant.Application.Services;

using Microsoft.OpenApi;

using LawAssistant.Infrastructure.FileStorage;
using LawAssistant.Infrastructure.RepositoryImplementation;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;
using LawAssistant.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { });
	c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey
	});
});

builder.Services.AddDbContext<PostgreSqlDbContext>(options =>
	options.UseNpgsql(builder.Configuration["DbConfiguration:PostreSqlConnectionString"]));

builder.Services.AddScoped<ILawyerRepository, LawyerRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ILawDocumentsRepository, LawDocumentsRepository>();

builder.Services.AddScoped<IFileService, LocalFileService>();

builder.Services.AddScoped<IHashService, SHA256HashService>();
builder.Services.AddScoped<ITokenProvider, JwtTokenProvider>();
builder.Services.AddScoped<IAuthentificationService, AuthentificationService>();

builder.Services.AddJwtAuthentification(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();

app.Run();