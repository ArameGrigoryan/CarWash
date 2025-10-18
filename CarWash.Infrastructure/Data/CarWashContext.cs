using CarWash.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Infrastructure.Data;

public class CarWashContext : DbContext
{
    public CarWashContext(DbContextOptions<CarWashContext> options) : base(options) {}
    
    public CarWashContext() { }
    
    public DbSet<Car> Cars { get; set; }
    
    public DbSet<User> Users { get; set; }
    
    public DbSet<Service> Services { get; set; }
    
    public DbSet<Booking> Bookings { get; set; }
    
    public DbSet<WashStation> WashStations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=CarWashDb;Username=postgres;Password=1111");
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 🔹 User → Car (1-to-many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Cars)
            .WithOne(c => c.User)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 User → Booking (1-to-many)
        modelBuilder.Entity<User>()
            .HasMany(u => u.Bookings)
            .WithOne(b => b.User)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 Car → Booking (1-to-many)
        modelBuilder.Entity<Car>()
            .HasMany(c => c.Bookings)
            .WithOne(b => b.Car)
            .HasForeignKey(b => b.CarId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 Service → Booking (1-to-many)
        modelBuilder.Entity<Service>()
            .HasMany<Booking>()
            .WithOne(b => b.Service)
            .HasForeignKey(b => b.ServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        // 🔹 WashStation → Booking (1-to-many)
        modelBuilder.Entity<WashStation>()
            .HasMany(ws => ws.Bookings)
            .WithOne(b => b.Station)
            .HasForeignKey(b => b.WashStationId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Booking>()
            .Property(b => b.Status)
            .HasConversion<int>(); // Enum → Int
    }
}