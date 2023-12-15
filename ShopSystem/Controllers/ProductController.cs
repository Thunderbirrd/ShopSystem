using Microsoft.AspNetCore.Mvc;
using ShopSystem.Model;
using ShopSystem.Services;

namespace ShopSystem.Controllers;


[ApiController]
[Route("api/products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;

    public ProductController(ProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public IActionResult CreateProduct([FromBody] Product product)
    {
        _productService.CreateProduct(product);
        return Ok();
    }
    
    [HttpPost("buy")]
    public IActionResult BuyProducts([FromBody] Request data)
    {
        var resp = _productService.BuyProducts(data.shopCode, data.products);
        return Ok(resp);
    }
    
    [HttpPost("find/cheapest")]
    public IActionResult FindCheapestShopForProducts([FromBody] IEnumerable<Product> products)
    {
        var resp = _productService.FindCheapestShopForProducts(products);
        return Ok(resp);
    }

    [HttpGet]
    public IActionResult GetProducts()
    {
        var products = _productService.GetProducts();
        return Ok(products);
    }
    
    [HttpGet("{shopCode}/{budget}")]
    public IActionResult GetAffordableProducts(int shopCode, int budget)
    {
        var products = _productService.GetAffordableProducts(shopCode, budget);
        return Ok(products);
    }

    [HttpGet("{shopCode}")]
    public IActionResult GetProductsByShopCode(int shopCode)
    {
        var products = _productService.GetProductsByShopCode(shopCode);
        return Ok(products);
    }

    [HttpPut("{shopCode}/{productName}/quantity/{newQuantity}")]
    public IActionResult UpdateProductQuantity(int shopCode, string productName, int newQuantity)
    {
        _productService.UpdateProductQuantity(shopCode, productName, newQuantity);
        return Ok();
    }

    [HttpPut("{shopCode}/{productName}/price/{newPrice}")]
    public IActionResult UpdateProductPrice(int shopCode, string productName, decimal newPrice)
    {
        _productService.UpdateProductPrice(shopCode, productName, newPrice);
        return Ok();
    }

    public new class Request
    {
        public int shopCode;
        public IEnumerable<Product> products;
    }
}