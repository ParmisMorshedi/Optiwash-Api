using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OptiWash.Models;
public class OptiWashDbContext : IdentityDbContext<User>
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<WashRecord> WashRecords { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    public OptiWashDbContext(DbContextOptions<OptiWashDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Car>()
            .Property(c => c.ScannedLicensePlate)
            .HasMaxLength(10);

        modelBuilder.Entity<WashRecord>()
            .HasOne(w => w.Car)
            .WithMany(c => c.WashHistory)
            .HasForeignKey(w => w.CarId);

       
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedDate = DateTime.UtcNow;
            }
            else if (entry.State == EntityState.Modified)
            {
                entry.Entity.UpdatedDate = DateTime.UtcNow;
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

}
