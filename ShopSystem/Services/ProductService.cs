using ShopSystem.Repo.Ifaces;
using ShopSystem.Model;

namespace ShopSystem.Services;


public class ProductService
{
    private readonly IProductRepository _productRepository;

    public ProductService(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public void CreateProduct(Product product)
    {
        _productRepository.CreateProduct(product);
    }

    public IEnumerable<Product>? GetProducts()
    {
        return _productRepository.GetProducts();
    }

    public IEnumerable<Product>? GetProductsByShopCode(int shopCode)
    {
        return _productRepository.GetProductsByShopCode(shopCode);
    }

    public void UpdateProductQuantity(int shopCode, string? productName, int newQuantity)
    {
        _productRepository.UpdateProductQuantity(shopCode, productName, newQuantity);
    }

    public void UpdateProductPrice(int shopCode, string productName, decimal newPrice)
    {
        _productRepository.UpdateProductPrice(shopCode, productName, newPrice);
    }

    public Shop? FindCheapestShopForProducts(IEnumerable<Product> products)
    {
        var shops = products
            .GroupBy(product => product.ShopCode)
            .Select(group => new
            {
                ShopCode = group.Key,
                TotalCost = group.Sum(product => product.Price * product.Quantity)
            }).MinBy(result => result.TotalCost);

        return shops != null ? new Shop { Code = shops.ShopCode } : null;
    }

    public IEnumerable<Product> GetAffordableProducts(int shopCode, decimal budget)
    {
        var affordableProducts = new List<Product>();
        var products = _productRepository.GetProductsByShopCode(shopCode);

        if (products == null) return affordableProducts;
        foreach (var product in products)
        {
            var totalCost = product.Price * product.Quantity;
            if (totalCost > budget) continue;
            affordableProducts.Add(product);
            budget -= totalCost;
        }

        return affordableProducts;
    }

    public decimal BuyProducts(int shopCode, IEnumerable<Product> productsToBuy)
    {
        decimal totalCost = 0;

        foreach (var productToBuy in productsToBuy)
        {
            var availableProduct = _productRepository?.GetProductsByShopCode(shopCode)!
                .FirstOrDefault(p => p.Name == productToBuy.Name && p.Quantity >= productToBuy.Quantity);

            if (availableProduct != null)
            {
                totalCost += availableProduct.Price * productToBuy.Quantity;
                _productRepository?.UpdateProductQuantity(shopCode, availableProduct.Name, availableProduct.Quantity - productToBuy.Quantity);
            }
            else
            {
                throw new InvalidOperationException($"Insufficient quantity of {productToBuy.Name} in shop {shopCode}");
            }
        }

        return totalCost;
    }
}