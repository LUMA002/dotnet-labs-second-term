using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Data.Configurations
{
    public sealed class TrainConfiguration : IEntityTypeConfiguration<Train>
    {

        public void Configure(EntityTypeBuilder<Train> builder) { 

            builder.ToTable(nameof(Train));
            
            builder.HasKey(t => t.TrainId); 
           
            builder.Property(t => t.TrainNumber)
                .IsRequired()
                .HasMaxLength(ModelConstants.TrainNumberMaxLength)
                .UseCollation("Latin1_General_CI_AS");  

            builder.HasIndex(t => t.TrainNumber).IsUnique();

            // Train-TrainType many to one relationship
            builder.HasOne(t => t.TrainType)
                .WithMany(tt => tt.Trains)
                .HasForeignKey(t => t.TrainTypeId)
                .OnDelete(DeleteBehavior.Restrict);

            // Train-Tickets one to many relationship
            //builder.HasMany(t => t.Tickets)
            //    .WithOne(ti => ti.Train)
            //    .HasForeignKey(ti => ti.TrainId)
            //    .OnDelete(DeleteBehavior.Restrict);

            // Train-Wagons one to many relationship
            //builder.HasMany(t => t.Wagons)
            //    .WithOne(w => w.Train)
            //    .HasForeignKey(w => w.TrainId)
            //    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
