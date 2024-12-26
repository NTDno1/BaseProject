using DemoCICD.Application.Abstractions.Behaviors;
using DemoCICD.Application.DependencyInjection.Extensions;
using FluentValidation;
using MediatR;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(options => options.RegisterServicesFromAssembly(DemoCICD.Application.AssemblyReference.Assembly));
builder.Services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
builder.Services.AddValidatorsFromAssembly(DemoCICD.Application.AssemblyReference.Assembly, includeInternalTypes: true);

//AddConfiguration
builder.Services.AddConfigureMediatR();
//End AddConfiguration
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
