using Microsoft.EntityFrameworkCore;

using LawAssistant.Application.Contracts;

using LawAssistant.Domain.Repositories;

using LawAssistant.Infrastructure.FileStorage;
using LawAssistant.Infrastructure.RepositoryImplementation;
using LawAssistant.Infrastructure.RepositoryImplementation.PostgreSqlRepo;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PostgreSqlDbContext>(options =>
	options.UseNpgsql(builder.Configuration["DbConfiguration:PostreSqlConnectionString"]));

builder.Services.AddScoped<ILawyerRepository, LawyerRepository>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<ILawDocumentsRepository, LawDocumentsRepository>();

builder.Services.AddScoped<IFileService, LocalFileService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();