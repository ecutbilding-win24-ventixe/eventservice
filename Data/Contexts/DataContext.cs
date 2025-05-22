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

}

