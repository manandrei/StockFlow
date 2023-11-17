using Microsoft.Extensions.DependencyInjection;

namespace StockFlow.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(options => { options.RegisterServicesFromAssemblies(typeof(DependencyInjection).Assembly); });

        return services;
    }
}