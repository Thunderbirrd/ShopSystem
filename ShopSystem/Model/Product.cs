using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Model;

public class Product
{
    public Product(int id, string? name, int shopCode, int quantity, decimal price)
    {
        Id = id;
        Name = name;
        ShopCode = shopCode;
        Quantity = quantity;
        Price = price;
    }

    public Product()
    {
    }

    [Key]
    public int Id { get; init; }
    public string? Name { get; set; }
    public int ShopCode { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
}