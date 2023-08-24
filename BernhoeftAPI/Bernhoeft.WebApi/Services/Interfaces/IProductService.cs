using Bernhoeft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.WebApi.Services.Interfaces
{
    public interface IProductService
    {
        Task<ActionResult<IEnumerable<Product>>> GetAll();
        Task<ActionResult<IEnumerable<Product>>> GetActiveProducts(bool active);
        Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name);
        Task<ActionResult<IEnumerable<Product>>> GetByCategoriesId(Guid categoryId);
        Task<ActionResult<Product>> Create(Product product);
        Task<ActionResult<Product>> UpdateProduct(Guid id, Product product);
    }
}