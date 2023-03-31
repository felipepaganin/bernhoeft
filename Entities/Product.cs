using Bernhoeft.Shared.Entities;
using System.Text.Json.Serialization;

namespace Bernhoeft.Domain.Entities;

public class Product : Entity
{
    protected Product() { }

    public Product(string name, string description, decimal price, Guid categoryId)
    {
        Name = name;
        Description = description;
        Price = price;
        CategoryId = categoryId;
        Active= true;
    }

    public string Name { get; private set; }
    public string Description { get; private set; }
    public decimal Price { get; private set; }
    public bool? Active { get; private set; }
    public Guid CategoryId { get; private set; }
    [JsonIgnore]
    public virtual Category? Category { get; private set; }

    public void Put(Product product)
    {
        Name = product.Name;
        Description = product.Description;
        Price = product.Price;
        CategoryId = product.CategoryId;
    }
}
