using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Locations;
using StockFlow.Domain.Materials;
using System.Text.Json;

namespace StockFlow.Infrastructure.Locations
{
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

            var jsonSerializerOptions = new JsonSerializerOptions { WriteIndented = false };
            builder.Property(p => p.SizeTypes)
                .HasConversion(
                    p => JsonSerializer.Serialize(p, jsonSerializerOptions),
                    pJson => JsonSerializer.Deserialize<HashSet<SizeType>>(pJson, jsonSerializerOptions)!);

            builder.HasIndex(p => new { p.RackId, p.Name })
                .IsUnique();
        }
    }
}
