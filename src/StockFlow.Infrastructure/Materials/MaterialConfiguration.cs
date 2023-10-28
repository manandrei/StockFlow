using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.Materials;

namespace StockFlow.Infrastructure.Materials
{
    public class MaterialConfiguration : IEntityTypeConfiguration<Material>
    {
        public void Configure(EntityTypeBuilder<Material> builder)
        {
            builder.HasKey(m => m.Id);
            builder.Property(m => m.Id).ValueGeneratedOnAdd();

            builder.Property(m => m.PartNumber)
                .IsRequired()
                .HasMaxLength(7);

            builder.Property(m => m.Description)
                .IsRequired(false)
                .HasMaxLength(250);

            builder.Property(m => m.SizeType)
                .IsRequired()
                .HasConversion(
                    st => st.ToString(),
                    stString => Enum.Parse<SizeType>(stString));

            builder.HasIndex(m => m.PartNumber)
                .IsUnique();
        }
    }
}
