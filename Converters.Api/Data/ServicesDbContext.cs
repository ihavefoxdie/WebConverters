using Microsoft.EntityFrameworkCore;
using Converters.Api.Entities;

namespace Converters.Api.Data;

public class ServicesDbContext: DbContext
{
    public DbSet<ServiceCategory> Categories { get; set; }
    public DbSet<Service> Services { get; set; }

    public ServicesDbContext(DbContextOptions<ServicesDbContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ServiceCategory>().HasData(new ServiceCategory
        {
            Id = 1,
            Name = "Main Services"
        });

        modelBuilder.Entity<Service>().HasData(new Service
        {
            Id = 1,
            CategoryId = 1,
            Name = "Placeholder",
            Description = "This is a test!\nThis is a test of the database and the app!"
        });
    }
}
