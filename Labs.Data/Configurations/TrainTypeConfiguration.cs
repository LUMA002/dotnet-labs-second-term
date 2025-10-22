using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Data.Configurations
{
    public sealed class TrainTypeConfiguration : IEntityTypeConfiguration<TrainType>
    {
        public void Configure(EntityTypeBuilder<TrainType> builder)
        {
            builder.ToTable("TrainType");
            builder.HasKey(tt => tt.TrainTypeId);

            builder.Property(tt => tt.TypeName)
                .IsRequired()
                .HasMaxLength(ModelConstants.TrainTypeNameMaxLength)
                .UseCollation("Latin1_General_CI_AS");
            
            builder.HasIndex(tt => tt.TypeName).IsUnique();

            // TrainType-Trains one to many relationship
            //builder.HasMany(tt => tt.Trains)
            //    .WithOne(t => t.TrainType)
            //    .HasForeignKey(t => t.TrainTypeId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
