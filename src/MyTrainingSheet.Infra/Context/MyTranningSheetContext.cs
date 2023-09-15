using Microsoft.EntityFrameworkCore;
using MyTrainingSheet.Domain;

namespace MyTrainingSheet.Infra.Context;

public class MyTranningSheetContext : DbContext
{
    public MyTranningSheetContext(DbContextOptions<MyTranningSheetContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
    public DbSet<Lift> lifts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Lift>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.ToTable("lifts");

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .ValueGeneratedNever();

            entity.Property(e => e.Name).HasColumnName("name")
            .HasColumnType("varchar(255)");

            entity.Property(e => e.Weight).HasColumnName("weight");

            entity.Property(e => e.Reps).HasColumnName("reps");

        });

        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(string))
                {
                    if (property.IsFixedLength() ?? false)
                    {
                        property.SetColumnType("varchar(max)");
                    }
                }
            }
        }

        base.OnModelCreating(modelBuilder);
    }
}
