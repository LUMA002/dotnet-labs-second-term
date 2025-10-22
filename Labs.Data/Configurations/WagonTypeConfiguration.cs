using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Labs.Data.Configurations
{
    public sealed class WagonTypeConfiguration : IEntityTypeConfiguration<WagonType>
    {
        public void Configure(EntityTypeBuilder<WagonType> builder)
        {
            builder.ToTable("WagonType");

            builder.HasKey(wt => wt.WagonTypeId);

            builder.Property(wt => wt.WagonTypeName)
                .IsRequired()
                .HasMaxLength(ModelConstants.WagonTypeNameMaxLength)
                .UseCollation("Latin1_General_CI_AS");

            builder.Property(wt => wt.Surcharge)
                .HasPrecision(ModelConstants.DecimalPrecision, ModelConstants.DecimalScale)
                .IsRequired();

            // WagonType-Wagons one to many relationship
            builder.HasMany(wt => wt.Wagons)
                .WithOne(w => w.WagonType)
                .HasForeignKey(w => w.WagonTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(wt => wt.WagonTypeName)
                .IsUnique();
        }
    }
}
