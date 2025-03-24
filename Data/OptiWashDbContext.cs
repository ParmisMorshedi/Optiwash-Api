using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class OptiWashDbContext : IdentityDbContext<User>
{
    public DbSet<Car> Cars { get; set; }
    public DbSet<WashRecord> WashRecords { get; set; }

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

        modelBuilder.Entity<WashRecord>()
            .HasOne(w => w.User)
            .WithMany(u => u.WashRecords)
            .HasForeignKey(w => w.UserId);
    }
}
