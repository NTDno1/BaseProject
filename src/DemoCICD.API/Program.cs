using DemoCICD.Application.DependencyInjection.Extensions;
using DemoCICD.Persistance.DependencyInjection.Extensions;
using DemoCICD.Persistance.DependencyInjection.Options;
using FluentValidation;
using MediatR;
using Serilog;
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();

//builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// Add configuration

Log.Logger = new LoggerConfiguration().ReadFrom
    .Configuration(builder.Configuration)
    .CreateLogger();

builder.Logging
    .ClearProviders()
    .AddSerilog();

builder.Host.UseSerilog();

builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(DemoCICD.Application.AssemblyReference.Assembly));
builder.Services.AddValidatorsFromAssembly(DemoCICD.Application.AssemblyReference.Assembly, includeInternalTypes: true);

builder.Services.AddConfigureMediatR();

// Configure Options and SQL
builder.Services.ConfigureSqlServerRetryOptions(builder.Configuration.GetSection(nameof(SqlServerRetryOptions)));
builder.Services.AddSqlConfiguration();
builder.Services.AddConfigurationAutoMapper();
builder.Services.AddRepositoryBaseConfiguration();

//Api

builder
    .Services
    .AddControllers()
    .AddApplicationPart(DemoCICD.Presentation.AssemblyReference.Assembly);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
