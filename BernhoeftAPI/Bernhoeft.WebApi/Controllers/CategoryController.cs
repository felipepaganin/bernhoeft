using Bernhoeft.Domain.Entities;
using Bernhoeft.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.WebApi.Controllers;

[ApiController]
[Route("api/[Controller]/")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _service;

    public CategoryController(ICategoryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
    {
        return await _service.GetAll();
    }

    [HttpGet("activecategories")]
    public async Task<ActionResult<IEnumerable<Category>>> GetActiveCategories(bool active)
    {
        return await _service.GetActiveCategories(active);
    }

    [HttpGet("categoriesbyname")]
    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesByName(string name)
    {
        return await _service.GetCategoriesByName(name);
    }

    [HttpPost]
    public async Task<ActionResult<Category>> CreateCategory(Category produto)
    {
        return await _service.Create(produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Category>> UpdateCategory(Guid id, Category produto)
    {
        return await _service.UpdateCategory(id, produto);
    }
}