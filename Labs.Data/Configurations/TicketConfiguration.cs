using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Labs.Data.Configurations
{
    public sealed class TicketConfiguration : IEntityTypeConfiguration<Ticket>
    {
        public void Configure(EntityTypeBuilder<Ticket> builder)
        {
            builder.ToTable("Ticket");
            builder.HasKey(t => t.TicketId);


            builder.Property(t => t.PassengerId)
                 .IsRequired();

            builder.Property(t => t.TrainId)
                .IsRequired();

            builder.Property(t => t.WagonId)
                .IsRequired();

            builder.Property(t => t.DestinationId)
                .IsRequired();


            builder.Property(t => t.DepartureDateTime)
                .IsRequired();

            builder.Property(t => t.ArrivalDateTime)
                .IsRequired();


            builder.Property(t => t.UrgencySurcharge)
                .HasPrecision(ModelConstants.DecimalPrecision, ModelConstants.DecimalScale)
                .IsRequired();

            builder.Property(t => t.TotalPrice)
                .HasPrecision(ModelConstants.DecimalPrecision, ModelConstants.DecimalScale)
                .IsRequired();

            // Relationships

            // Ticket-Train many to one relationship
            builder.HasOne(t => t.Train)
                .WithMany(tr => tr.Tickets)
                .HasForeignKey(t => t.TrainId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket-Passenger many to one relationship
            builder.HasOne(t => t.Passenger)
                .WithMany(p => p.Tickets)
                .HasForeignKey(t => t.PassengerId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket-Wagon many to one relationship
            builder.HasOne(t => t.Wagon)
                .WithMany(w => w.Tickets)
                .HasForeignKey(t => t.WagonId)
                .OnDelete(DeleteBehavior.Restrict);

            // Ticket-Destination many to one relationship
            builder.HasOne(t => t.Destination)
                .WithMany(d => d.Tickets)
                .HasForeignKey(t => t.DestinationId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.PassengerId);
            builder.HasIndex(t => t.TrainId);
            builder.HasIndex(t => t.DepartureDateTime);
            builder.HasIndex(t => new { t.TrainId, t.DepartureDateTime });
        }
    }
}
