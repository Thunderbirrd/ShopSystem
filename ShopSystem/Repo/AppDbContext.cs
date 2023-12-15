using Microsoft.EntityFrameworkCore;
using ShopSystem.Model;

namespace ShopSystem.Repo;

public class AppDbContext : DbContext
{
    public DbSet<Shop>? Shops { get; set; }
    public DbSet<Product>? Products { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var dbPath = Path.Combine(Directory.GetCurrentDirectory(), "app.sqlite");
        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }

    public void ApplyMigrations()
    {
        var migrationsPath = Path.Combine(Directory.GetCurrentDirectory(), "Migrations");
        
        if (!Directory.Exists(migrationsPath)) return;
        foreach (var migrationFile in Directory.GetFiles(migrationsPath, "*.sql"))
        {
            var sqlScript = File.ReadAllText(migrationFile);
            
            Database.ExecuteSqlRaw(sqlScript);
        }
    }
}