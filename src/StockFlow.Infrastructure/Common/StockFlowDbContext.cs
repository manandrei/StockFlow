using Microsoft.EntityFrameworkCore;
using StockFlow.Domain.ActionLogs;
using StockFlow.Domain.Locations;
using StockFlow.Domain.Materials;
using StockFlow.Domain.Stocks;
using System.Reflection;

namespace StockFlow.Infrastructure.Common;

public class StockFlowDbContext : DbContext
{
    public DbSet<Material> Materials { get; set; }
    public DbSet<Rack> Racks { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<ActionLog> ActionLogs { get; set; }


    public StockFlowDbContext(DbContextOptions<StockFlowDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}