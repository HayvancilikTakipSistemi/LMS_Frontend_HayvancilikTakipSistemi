using LMS.Server.Models;
using LMS.Shared.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LMS.Server.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Grup A & B Entity DbSets
    public DbSet<Animal> Animals { get; set; }
    public DbSet<Farmer> Farmers { get; set; }
    public DbSet<Breed> Breeds { get; set; }
    public DbSet<AnimalStatus> AnimalStatuses { get; set; }
    public DbSet<Barn> Barns { get; set; }
    public DbSet<AnimalType> AnimalTypes { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Cascade delete'i kapatma - referans verileri silmeyi önle
        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Animal Self-Referencing Configuration (Fluent API)
        modelBuilder.Entity<Animal>()
            .HasOne(a => a.MotherAnimal)
            .WithMany(a => a.OffspringAnimals)
            .HasForeignKey(a => a.MotherAnimalID)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);

        // SEED DATA: AnimalStatus
        var statuses = new AnimalStatus[]
        {
            new AnimalStatus { AnimalStatusID = 1, StatusName = "Aktif", Description = "Sağlıklı ve aktif hayvan" },
            new AnimalStatus { AnimalStatusID = 2, StatusName = "Hasta", Description = "Veteriner kontrolünde" },
            new AnimalStatus { AnimalStatusID = 3, StatusName = "Gebe", Description = "Doğum bekliyor" },
            new AnimalStatus { AnimalStatusID = 4, StatusName = "Satıldı", Description = "Satış gerçekleştirildi" },
            new AnimalStatus { AnimalStatusID = 5, StatusName = "Öldü", Description = "Hayvan hayatını kaybetti" },
            new AnimalStatus { AnimalStatusID = 6, StatusName = "Karantina", Description = "Karantina altında" }
        };
        modelBuilder.Entity<AnimalStatus>().HasData(statuses);

        // SEED DATA: AnimalType
        var animalTypes = new AnimalType[]
        {
            new AnimalType { AnimalTypeID = 1, TypeName = "Sığır", Description = "Büyükbaş" },
            new AnimalType { AnimalTypeID = 2, TypeName = "Koyun", Description = "Küçükbaş" },
            new AnimalType { AnimalTypeID = 3, TypeName = "Keçi", Description = "Küçükbaş" },
            new AnimalType { AnimalTypeID = 4, TypeName = "Manda", Description = "Büyükbaş" }
        };
        modelBuilder.Entity<AnimalType>().HasData(animalTypes);

        // SEED DATA: Breed
        var breeds = new Breed[]
        {
            new Breed { BreedID = 1, AnimalTypeID = 1, BreedName = "Holstein", MilkCapacityLiters = 30M, Description = "Yüksek süt üreticisi" },
            new Breed { BreedID = 2, AnimalTypeID = 1, BreedName = "Montofon", MilkCapacityLiters = 22M, Description = "Orta süt üreticisi" },
            new Breed { BreedID = 3, AnimalTypeID = 2, BreedName = "Merinos", MilkCapacityLiters = null, Description = "In üreticisi" },
            new Breed { BreedID = 4, AnimalTypeID = 3, BreedName = "Saanen", MilkCapacityLiters = 4.5M, Description = "Süt keçisi" }
        };
        modelBuilder.Entity<Breed>().HasData(breeds);
    }
}
