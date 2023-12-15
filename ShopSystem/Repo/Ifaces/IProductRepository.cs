using ShopSystem.Model;

namespace ShopSystem.Repo.Ifaces;

public interface IProductRepository
{
    void CreateProduct(Product product);
    Product? GetProductInShop(int shopCode, string? productName);
    IEnumerable<Product>? GetProducts();
    IEnumerable<Product>? GetProductsByShopCode(int shopCode);
    void UpdateProductQuantity(int shopCode, string? productName, int newQuantity);
    void UpdateProductPrice(int shopCode, string productName, decimal newPrice);
}