using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StockFlow.Application.ActionLogs;
using StockFlow.Application.Common;
using StockFlow.Application.Locations;
using StockFlow.Application.Materials;
using StockFlow.Application.Stocks;
using StockFlow.Infrastructure.ActionLogs;
using StockFlow.Infrastructure.Common;
using StockFlow.Infrastructure.Locations;
using StockFlow.Infrastructure.Materials;
using StockFlow.Infrastructure.Stocks;

namespace StockFlow.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string? connectionString)
    {
        if (string.IsNullOrWhiteSpace(connectionString))
            throw new ArgumentNullException(nameof(connectionString), "Invalid connection string argument");

        services.AddDbContext<StockFlowDbContext>(options => { options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)); });
        services.AddScoped<IActionLogRepository, ActionLogRepository>();
        services.AddScoped<IPositionRepository, PositionRepository>();
        services.AddScoped<IRackRepository, RackRepository>();
        services.AddScoped<IMaterialRepository, MaterialRepository>();
        services.AddScoped<IStockRepository, StockRepository>();
        services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<StockFlowDbContext>());

        return services;
    }
}