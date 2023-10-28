using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Locations;

namespace StockFlow.Infrastructure.Locations
{
    public class RackConfiguration : IEntityTypeConfiguration<Rack>
    {
        public void Configure(EntityTypeBuilder<Rack> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.Id).ValueGeneratedOnAdd();
            
            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(10);
            
            builder.HasIndex(r => r.Name)
                .IsUnique();
        }
    }
}
