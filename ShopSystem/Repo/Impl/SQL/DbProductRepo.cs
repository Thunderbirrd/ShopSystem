using ShopSystem.Model;
using ShopSystem.Repo.Ifaces;

namespace ShopSystem.Repo.Impl.SQL;

public class DbProductRepo : IProductRepository
{
    private readonly AppDbContext _dbContext;

    public DbProductRepo(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void CreateProduct(Product product)
    {
        _dbContext.Products?.Add(product);
        _dbContext.SaveChanges();
    }

    public Product? GetProductInShop(int shopCode, string? productName)
    {
        return _dbContext.Products?.Where(p => p.ShopCode == shopCode && p.Name == productName).FirstOrDefault();
    }

    public IEnumerable<Product>? GetProducts()
    {
        return _dbContext.Products?.ToList();
    }

    public IEnumerable<Product>? GetProductsByShopCode(int shopCode)
    {
        return _dbContext.Products?.Where(p => p.ShopCode == shopCode).ToList();
    }

    public void UpdateProductQuantity(int shopCode, string? productName, int newQuantity)
    {
        var product = _dbContext.Products?
            .FirstOrDefault(p => p.ShopCode == shopCode && p.Name == productName);

        if (product == null) return;
        product.Quantity = newQuantity;
        _dbContext.SaveChanges();
    }

    public void UpdateProductPrice(int shopCode, string productName, decimal newPrice)
    {
        var product = _dbContext.Products?
            .FirstOrDefault(p => p.ShopCode == shopCode && p.Name == productName);

        if (product == null) return;
        product.Price = newPrice;
        _dbContext.SaveChanges();
    }
}