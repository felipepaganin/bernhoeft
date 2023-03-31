using Bernhoeft.Shared.Entities;
using System.Text.Json.Serialization;

namespace Bernhoeft.Domain.Entities
{
    public class Category : Entity
    {

        public Category(string name)
        {
            Name = name;
            Active = true;
        }

        public string? Name { get; set; }
        public bool? Active { get; set; }

        [JsonIgnore]
        public virtual ICollection<Product>? Products { get; set; }

        public void Put(Category category)
        {
            Name = category.Name;
            Active = category.Active;
        }
    }
}
