using ShopSystem.Model;

namespace ShopSystem.Repo.Ifaces;

public interface IShopRepository
{
    void CreateShop(Shop shop);
    IEnumerable<Shop>? GetShops();
}