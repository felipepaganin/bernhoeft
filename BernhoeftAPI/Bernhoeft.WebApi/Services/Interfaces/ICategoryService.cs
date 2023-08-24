using Bernhoeft.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.WebApi.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ActionResult<IEnumerable<Category>>> GetAll();
        Task<ActionResult<IEnumerable<Category>>> GetActiveCategories(bool active);
        Task<ActionResult<IEnumerable<Category>>> GetCategoriesByName(string name);
        Task<ActionResult<Category>> Create(Category category);
        Task<ActionResult<Category>> UpdateCategory(Guid id, Category category);
    }
}