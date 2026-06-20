using Microsoft.EntityFrameworkCore;

using Microsoft.OpenApi;

using LawAssistant.Infrastructure;
using LawAssistant.Application;

using LawAssistant.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

if(builder.Environment.IsDevelopment())
{
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
}

builder.Services.AddCors(options =>
{
	options.AddDefaultPolicy(policy =>
	{
		policy.WithOrigins("http://localhost:3000");
		policy.AllowAnyHeader();
		policy.AllowAnyMethod();
	});
});

builder.Services.AddHttpClient();
builder.Services.AddHttpContextAccessor();

builder.Services.AddJwtAuthentification(builder.Configuration);

builder.Services.AddApplicationLayer();
builder.Services.AddInfrastructureLayer(builder.Configuration);


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