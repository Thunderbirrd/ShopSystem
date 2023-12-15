using ShopSystem.Model;
using ShopSystem.Repo.Ifaces;

namespace ShopSystem.Repo.Impl.CSV;

public class ShopCsvRepository : IShopRepository
{
    private readonly string _csvFilePath;

    public ShopCsvRepository(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
    }

    public void CreateShop(Shop shop)
    {
        var csvLine = $"{shop.Code},{shop.Name}";
        File.AppendAllLines(_csvFilePath, new[] { csvLine });
    }

    public IEnumerable<Shop> GetShops()
    {
        var shops = File.ReadAllLines(_csvFilePath)
            .Select(line => line.Split(','))
            .Select(parts => new Shop
            {
                Code = int.Parse(parts[0]),
                Name = parts[1]
            });

        return shops.ToList();
    }
}