using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Data.Configurations
{
    public sealed class WagonConfiguration : IEntityTypeConfiguration<Wagon>
    {
        public void Configure(EntityTypeBuilder<Wagon> builder)
        {
            builder.ToTable("Wagon");

            builder.HasKey(w => w.WagonId);

            builder.Property(w => w.WagonNumber)
                .IsRequired()
                .HasMaxLength(ModelConstants.WagonNumberMaxLength)
                .UseCollation("Latin1_General_CI_AS");

            // Wagon-Train many to one relationship
            builder.HasOne(w => w.Train)
                .WithMany(t => t.Wagons)
                .HasForeignKey(w => w.TrainId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(w => new { w.TrainId, w.WagonNumber })
                .IsUnique();
        }
    }
}
