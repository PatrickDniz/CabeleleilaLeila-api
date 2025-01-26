using Microsoft.EntityFrameworkCore;
using Scheduling.Models;

namespace Scheduling.Data;

public class SchedulingContext : DbContext
{
    public DbSet<SchedulingModel> Scheduling { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=scheduling.sqlite");
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SchedulingModel>()
            .HasKey(s => s.Id); 

        modelBuilder.Entity<SchedulingModel>()
            .Property(s => s.Id)
            .ValueGeneratedOnAdd();
    }
}