using System.Collections;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Locations;
using StockFlow.Domain.Materials;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace StockFlow.Infrastructure.Locations;

public class PositionConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.Property(p => p.Name)
            .IsRequired()
            .HasMaxLength(10);

        builder.Property(p => p.MaxAllowed)
            .IsRequired();

        builder.Property(p => p.MinAlert)
            .IsRequired();

        builder.Property(p => p.RackId)
            .IsRequired();

        builder.HasOne(p => p.Rack)
            .WithMany(r => r.Positions)
            .HasForeignKey(p => p.RackId);

        builder.HasMany(p => p.ExclusiveMaterials)
            .WithMany(m => m.Positions)
            .UsingEntity("PositionExclusivMaterials");

        var jsonOption = new JsonSerializerOptions{ WriteIndented = true };
        // Note: This is just for demonstration purposes as the SizeType could be defined as a domain type for a more optimal solution.
        builder.Property(p => p.SizeTypes)
            .HasConversion(
                v => JsonSerializer.Serialize(v, jsonOption),
                v => JsonSerializer.Deserialize<List<SizeType>>(v, jsonOption)!,
                new ValueComparer<List<SizeType>>(
                    (c1, c2) => c2 != null && c1 != null && c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToList()
                )
            );

        builder.HasIndex(p => new { p.RackId, p.Name })
            .IsUnique();
    }
}