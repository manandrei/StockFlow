using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StockFlow.Domain.ActionLogs;

namespace StockFlow.Infrastructure.ActionLogs
{
    public class ActionLogConfiguration : IEntityTypeConfiguration<ActionLog>
    {
        public void Configure(EntityTypeBuilder<ActionLog> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.Id).ValueGeneratedOnAdd();

            builder.Property(a => a.ActionType)
                .IsRequired()
                .HasConversion(
                    a => a.ToString(),
                    aString => Enum.Parse<ActionType>(aString)
                );
        }
    }
}
