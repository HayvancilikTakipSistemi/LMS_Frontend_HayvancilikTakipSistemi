using Microsoft.EntityFrameworkCore;
using LMS.Shared.Entities;

namespace LMS.Server.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Hayvan> Hayvanlar { get; set; }
    public DbSet<Ciftci> Ciftciler { get; set; }
    public DbSet<Kullanici> Kullanicilar { get; set; }
    
    // As per user provided SQL schema, we are defining exactly what we mapped
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Disable cascading deletes to maintain data integrity
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Kullanici configurations
        modelBuilder.Entity<Kullanici>()
            .HasIndex(u => u.KullaniciAdi)
            .IsUnique();

        modelBuilder.Entity<Kullanici>()
            .HasOne(u => u.BagliCiftci)
            .WithMany()
            .HasForeignKey(u => u.BagliCiftciID);

        // SQL mappings
        modelBuilder.Entity<Hayvan>().ToTable("Hayvan");
        modelBuilder.Entity<Ciftci>().ToTable("Ciftci");
        modelBuilder.Entity<Kullanici>().ToTable("Kullanici");

        // Seed data
        modelBuilder.Entity<Ciftci>().HasData(
            new Ciftci { CiftciID = 1, Ad = "Sistem", Soyad = "Çiftçisi", KayitTarihi = new DateTime(2026, 1, 1) }
        );
    }
}
