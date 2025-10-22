using Labs.Domain.Entities;
using Labs.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Labs.Data.Configurations
{
    public sealed class PassengerConfiguration : IEntityTypeConfiguration<Passenger>
    {
        public void Configure(EntityTypeBuilder<Passenger> builder)
        {
            builder.ToTable("Passenger");

            builder.HasKey(p => p.PassengerId);

            builder.Property(p => p.FirstName)
                .IsRequired().HasMaxLength(ModelConstants.PassengerNameMaxLength);

            builder.Property(p => p.LastName)
                .IsRequired().HasMaxLength(ModelConstants.PassengerNameMaxLength);

            builder.Property(p => p.MiddleName).HasMaxLength(ModelConstants.PassengerNameMaxLength);

            builder.Property(p => p.Address).HasMaxLength(ModelConstants.PassengerAddressMaxLength);

            builder.Property(p => p.PhoneNumber).HasMaxLength(ModelConstants.PassengerPhoneMaxLength);

            // Passenger-tickets one to many relationship
            //builder.HasMany(p => p.Tickets)
            //    .WithOne(t => t.Passenger)
            //    .HasForeignKey(t => t.PassengerId)
            //    .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(p => new { p.LastName, p.FirstName });
        }
    }
}
