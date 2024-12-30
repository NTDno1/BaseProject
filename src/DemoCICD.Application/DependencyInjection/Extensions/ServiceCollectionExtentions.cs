using DemoCICD.Application.Abstractions.Behaviors;
using DemoCICD.Application.Mapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Application.DependencyInjection.Extensions;
public static class ServiceCollectionExtentions
{
    public static IServiceCollection AddConfigureMediatR(this IServiceCollection services)
    {
        return services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(AssemblyReference.Assembly);
        }).AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>))
        .AddValidatorsFromAssembly(Contract.AssemblyReference.Assembly, includeInternalTypes: true);
    }

    public static IServiceCollection AddConfigurationAutoMapper(this IServiceCollection services)
    {
        return services.AddAutoMapper(typeof(ServiceProfile));
    }
}
