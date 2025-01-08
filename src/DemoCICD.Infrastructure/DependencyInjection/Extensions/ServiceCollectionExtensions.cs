using DemoCICD.Domain.Abstractions.Dappers;
using DemoCICD.Domain.Abstractions.Dappers.Repositories.Product;
using DemoCICD.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace DemoCICD.Infrastructure.DependencyInjection.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddInfrastructureDapper(this IServiceCollection services)
    {
        services.AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IUnitOfWork, UnitOfWork>();
    }
}
