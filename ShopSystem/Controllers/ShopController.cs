using ShopSystem.Model;
using Microsoft.AspNetCore.Mvc;
using ShopSystem.Services;

namespace ShopSystem.Controllers;


[ApiController]
[Route("api/shops")]
public class ShopController : ControllerBase
{
    private readonly ShopService _shopService;

    public ShopController(ShopService shopService)
    {
        _shopService = shopService;
    }

    [HttpPost]
    public IActionResult CreateShop([FromBody] Shop shop)
    {
        _shopService.CreateShop(shop);
        return Ok();
    }

    [HttpPost("{shopCode}/products")]
    public IActionResult ImportProductsToShop(int shopCode, [FromBody] IEnumerable<Product> products)
    {
        _shopService.ImportProductsToShop(shopCode, products);
        return Ok();
    }

    [HttpGet("cheapest/{productName}")]
    public IActionResult FindCheapestShop(string productName)
    {
        var cheapestShop = _shopService.FindCheapestShop(productName);
        return cheapestShop != null ? Ok(cheapestShop) : NotFound();
    }
    
}