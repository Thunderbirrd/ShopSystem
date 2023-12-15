using ShopSystem.Repo;
using ShopSystem.Repo.Ifaces;
using ShopSystem.Repo.Impl.CSV;
using ShopSystem.Repo.Impl.SQL;
using ShopSystem.Services;

namespace ShopSystem;

public class Startup
{
    private IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var useDatabase = Configuration.GetValue<bool>("use_db");
        
        if (useDatabase)
        {
            services.AddDbContext<AppDbContext>();
            
            services.AddScoped<IShopRepository, DbShopRepo>();
            services.AddScoped<IProductRepository, DbProductRepo>();
        }
        else
        {
            services.AddScoped<IShopRepository, ShopCsvRepository>();
            services.AddScoped<IProductRepository, ProductCsvRepository>();
        }
        
        services.AddScoped<ShopService, ShopService>();
        services.AddScoped<ProductService, ProductService>();
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        using (var serviceScope = app.ApplicationServices.CreateScope())
        {
            var dbContext = serviceScope.ServiceProvider.GetRequiredService<AppDbContext>();
            dbContext.ApplyMigrations();
        }

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
