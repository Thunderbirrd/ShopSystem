using ShopSystem.Model;
using ShopSystem.Repo.Ifaces;

namespace ShopSystem.Repo.Impl.SQL;

public class DbShopRepo : IShopRepository
{
    private readonly AppDbContext _dbContext;

    public DbShopRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateShop(Shop shop)
    {
        _dbContext.Shops?.Add(shop);
        _dbContext.SaveChanges();
    }

    public IEnumerable<Shop>? GetShops()
    {
        return _dbContext.Shops?.ToList();
    }
}