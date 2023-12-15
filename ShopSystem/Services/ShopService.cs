using ShopSystem.Model;
using ShopSystem.Repo.Ifaces;

namespace ShopSystem.Services;

public class ShopService
{
    private readonly IShopRepository _shopRepository;
    private readonly IProductRepository _productRepository;

    public ShopService(IShopRepository shopRepository, IProductRepository productRepository)
    {
        _shopRepository = shopRepository;
        _productRepository = productRepository;
    }

    public void CreateShop(Shop shop)
    {
        _shopRepository.CreateShop(shop);
    }

    public void CreateProduct(Product product)
    {
        _productRepository.CreateProduct(product);
    }

    public void ImportProductsToShop(int shopCode, IEnumerable<Product> products)
    {
        foreach (var product in products)
        {
            if (_productRepository.GetProductInShop(shopCode, product.Name)!.Id == 0) 
            {
                _productRepository.CreateProduct(product);
            }
            else
            {
                _productRepository.UpdateProductQuantity(shopCode, product.Name, product.Quantity);
            }
            
        }
    }

    public Shop? FindCheapestShop(string productName)
    {
        var cheapestShopCode = (_productRepository.GetProducts()!
            .Where(product => product.Name == productName)
            .GroupBy(product => product.ShopCode)
            .Select(group => new
            {
                ShopCode = group.Key,
                CheapestProductPrice = group.Min(product => product.Price)
            })).MinBy(result => result.CheapestProductPrice)?.ShopCode;

        return cheapestShopCode != null ? new Shop { Code = cheapestShopCode.Value } : null;
    }
}