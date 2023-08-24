using Bernhoeft.Domain.Entities;
using Bernhoeft.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Bernhoeft.WebApi.Controllers;

[Route("api/[Controller]/")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;

    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _service.GetAll();
    }

    [HttpGet("activeproducts")]
    public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts(bool active)
    {
        return await _service.GetActiveProducts(active);
    }

    [HttpGet("productsbyname")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
    {
        return await _service.GetProductsByName(name);
    }

    [HttpGet("productsbycategories")]
    public async Task<ActionResult<IEnumerable<Product>>> GetProductsByCategoriesId(Guid categoryId)
    {
        return await _service.GetByCategoriesId(categoryId);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product produto)
    {
        return await _service.Create(produto);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Product>> UpdateProduto(Guid id, Product produto)
    {
        return await _service.UpdateProduct(id, produto);
    }
}