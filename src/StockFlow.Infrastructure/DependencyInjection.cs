using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockFlow.Infrastructure.Common;

namespace StockFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString), "Invalid connection string argument");

        services.AddDbContext<StockFlowDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
        });

        return services;
    }
}
