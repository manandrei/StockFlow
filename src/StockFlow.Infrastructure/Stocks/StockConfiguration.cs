using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StockFlow.Domain.Stocks;

namespace StockFlow.Infrastructure.Stocks;

public class StockConfiguration : IEntityTypeConfiguration<Stock>
{
    public void Configure(EntityTypeBuilder<Stock> builder)
    {
        builder.HasKey(s => s.Id);
        builder.Property(s => s.Id).ValueGeneratedNever();

        builder.Property(s => s.TimeStamp)
            .IsRequired()
            .HasConversion<DateTimeOffsetToStringConverter>();

        builder.Property(s => s.ExpireDate)
            .IsRequired();

        builder.Property(s => s.BatchDate)
            .IsRequired();

        builder.Property(s => s.MaterialId)
            .IsRequired();
        
        builder.Property(s => s.PositionId).IsRequired();

        builder.HasOne(s => s.Material)
            .WithMany(m => m.Stocks)
            .HasForeignKey(s => s.MaterialId);

        builder.HasOne(s => s.Position)
            .WithMany(p => p.Stocks)
            .HasForeignKey(s => s.PositionId);
    }
}