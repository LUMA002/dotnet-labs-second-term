using Microsoft.EntityFrameworkCore;
using Labs.Domain.Entities;
using Labs.Data.SeedData;

namespace Labs.Data;

public class ReservationContext : DbContext
{
    public ReservationContext(DbContextOptions<ReservationContext> options) : base(options) { }

    public DbSet<Passenger> Passengers => Set<Passenger>();
    public DbSet<Train> Trains => Set<Train>();
    public DbSet<TrainType> TrainTypes => Set<TrainType>();
    public DbSet<Wagon> Wagons => Set<Wagon>();
    public DbSet<WagonType> WagonTypes => Set<WagonType>();
    public DbSet<Destination> Destinations => Set<Destination>();
    public DbSet<Ticket> Tickets => Set<Ticket>();

    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    base.OnConfiguring(optionsBuilder);

    //    optionsBuilder.UseSqlServer(
    //}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      
        base.OnModelCreating(modelBuilder);

        // modelBuilder.UseCollation change (configuration)
         

        // Automatically apply all configurations from the current assembly
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ReservationContext).Assembly);

        DatabaseSeeder.SeedData(modelBuilder);
    }
}