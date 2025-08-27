using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions options): IdentityDbContext<User>(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>()
            .Property(b => b.Status)
            .HasConversion<string>();

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Booking> Bookings { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Spot> Spots { get; set; }
}