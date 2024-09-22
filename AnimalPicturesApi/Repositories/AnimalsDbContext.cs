using AnimalPicturesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalPicturesApi.Repositories;

public class AnimalsDbContext: DbContext
{
    public DbSet<AnimalPicture> AnimalPictures { get; set; }
    
    public AnimalsDbContext(DbContextOptions<AnimalsDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Map entities to tables
        modelBuilder.Entity<AnimalPicture>().ToTable("AnimalPictures");
        
        // Configure Primary Keys
        modelBuilder.Entity<AnimalPicture>().HasKey(ai => ai.Id).HasName("PK_AnimalPictures");
        
        // Configure columns
        modelBuilder.Entity<AnimalPicture>().Property(ai => ai.Id).HasColumnType("int").UseMySqlIdentityColumn().IsRequired();
        modelBuilder.Entity<AnimalPicture>().Property(ai => ai.AnimalType).HasColumnType("varchar(10)").IsRequired();
        modelBuilder.Entity<AnimalPicture>().Property(ai => ai.Url).HasColumnType("varchar(500)").IsRequired();
    }
}