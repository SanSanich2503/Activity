using Core.Entities.Categories;
using Core.Entities.Goods;
using Core.Entities.GoodToCategories;
using Core.Entities.Purchases;
using Core.Entities.PurchaseStatuses;
using Core.Entities.Roles;
using Core.Entities.Users;
using Data.Models.AppSettings;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Core;

public class DataContext : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Good> Goods { get; set; }
    public DbSet<GoodToCategory> GoodToCategories { get; set; }
    public DbSet<Purchase> Purchases { get; set; }
    public DbSet<PurchaseStatus> PurchaseStatuses { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    
    public DataContext() {}
    
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(GetConnectionString());
    
    private string GetConnectionString()
    {
        var connectionString = "";

        var parentDirectory = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).FullName);
        var settingsFile = @$"{parentDirectory}\InternetShopApp\appsettings.json";

        if (File.Exists(settingsFile))
        {
            var json = File.ReadAllText(settingsFile);
            var model = JsonConvert.DeserializeObject<AppSettingsModel>(json);
            connectionString = model?.ConnectionStrings?.DefaultConnection ?? "";
        }

        return connectionString;
    }
}