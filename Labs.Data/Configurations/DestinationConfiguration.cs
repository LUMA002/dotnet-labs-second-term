using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Data.Configurations
{
    public sealed class DestinationConfiguration : IEntityTypeConfiguration<Destination>
    {
        public void Configure(EntityTypeBuilder<Destination> builder)
        {
            builder.ToTable(nameof(Destination));

            builder.HasKey(d => d.DestinationId);

            builder.Property(d => d.DestinationName)
                .IsRequired()
                .HasMaxLength(ModelConstants.DestinationNameMaxLength)
                .UseCollation("Latin1_General_CI_AS");

            builder.Property(d => d.Distance).IsRequired();

            builder.Property(d => d.BasePrice)
                 .HasPrecision(ModelConstants.DecimalPrecision, ModelConstants.DecimalScale)
                 .IsRequired();

            builder.HasIndex(d => d.DestinationName).IsUnique();

            builder.HasIndex(d => d.Distance);
        }
    }
}
