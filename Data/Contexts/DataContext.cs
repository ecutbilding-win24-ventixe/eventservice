using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
    public DbSet<EventCategoryEntity> EventCategories { get; set; }
    public DbSet<EventStatusEntity> EventStatuses { get; set; }
    public DbSet<EventPackageEntity> EventPackages { get; set; }
    public DbSet<EventPackageTypeEntity> EventPackagesType { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventPackageEntity>()
            .HasOne(p => p.Event)
            .WithMany(e => e.Packages)
            .HasForeignKey(p => p.EventId)
            .OnDelete(DeleteBehavior.Cascade);
    }

}

