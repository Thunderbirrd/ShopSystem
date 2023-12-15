using ShopSystem.Model;
using ShopSystem.Repo.Ifaces;

namespace ShopSystem.Repo.Impl.CSV;

public class ProductCsvRepository : IProductRepository
{
    private readonly string _csvFilePath;

    public ProductCsvRepository(string csvFilePath)
    {
        _csvFilePath = csvFilePath;
    }

    public void CreateProduct(Product product)
    {
        var csvLine = $"{product.Name},{product.ShopCode},{product.Quantity},{product.Price}";
        File.AppendAllLines(_csvFilePath, new[] { csvLine });
    }

    public Product? GetProductInShop(int shopCode, string? productName)
    {
        var products = GetProducts().Where(p => p.ShopCode == shopCode && p.Name == productName);

        return products.FirstOrDefault();
    }

    public IEnumerable<Product> GetProducts()
    {
        var products = File.ReadAllLines(_csvFilePath)
            .Select(line => line.Split(','))
            .Select(parts => new Product
            {
                Name = parts[0],
                ShopCode = int.Parse(parts[1]),
                Quantity = int.Parse(parts[2]),
                Price = decimal.Parse(parts[3])
            });

        return products.ToList();
    }

    public IEnumerable<Product> GetProductsByShopCode(int shopCode)
    {
        var products = GetProducts().Where(p => p.ShopCode == shopCode);

        return products.ToList();
    }

    public void UpdateProductQuantity(int shopCode, string? productName, int newQuantity)
    {
        var products = GetProducts();
        var enumerable = products as Product[] ?? products.ToArray();
        var product = enumerable.FirstOrDefault(p => p.ShopCode == shopCode && p.Name == productName);

        if (product == null) return;
        {
            product.Quantity = newQuantity;
            
            File.WriteAllLines(_csvFilePath, enumerable.Select(p => $"{p.Name},{p.ShopCode},{p.Quantity},{p.Price}"));
        }
    }

    public void UpdateProductPrice(int shopCode, string productName, decimal newPrice)
    {
        var products = GetProducts();
        var enumerable = products as Product[] ?? products.ToArray();
        var product = enumerable.FirstOrDefault(p => p.ShopCode == shopCode && p.Name == productName);

        if (product == null) return;
        {
            product.Price = newPrice;
            
            File.WriteAllLines(_csvFilePath, enumerable.Select(p => $"{p.Name},{p.ShopCode},{p.Quantity},{p.Price}"));
        }
    }
}
