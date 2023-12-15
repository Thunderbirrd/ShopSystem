using System.ComponentModel.DataAnnotations;

namespace ShopSystem.Model;

public class Shop
{
    public Shop(int code, string? name)
    {
        Code = code;
        Name = name;
    }

    public Shop()
    {
    }

    [Key]
    public int Code { get; init; }
    public string? Name { get; set; }
}