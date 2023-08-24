using Bernhoeft.Domain.Entities;
using Bernhoeft.Infra.Data;
using Bernhoeft.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.WebApi.Services;
public class CategoryService : ICategoryService
{
    private readonly IWriteRepository<Category> _writeRepository;
    private readonly IReadRepository<Category> _readRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IWriteRepository<Category> writeRepository, IReadRepository<Category> readRepository, IUnitOfWork unitOfWork)
    {
        _writeRepository = writeRepository;
        _readRepository = readRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ActionResult<Category>> Create(Category category)
    {
        var result = new Category(category.Name);

        await _writeRepository.AddAsync(result);
        await _unitOfWork.CommitAsync();
        return result;
    }

    public async Task<ActionResult<IEnumerable<Category>>> GetAll()
    {
        return await _readRepository.FindAll().ToListAsync();
    }

    public async Task<ActionResult<IEnumerable<Category>>> GetActiveCategories(bool active)
    {
        var result = await _readRepository.FindByCondition(x => x.Active == active).ToListAsync();

        if (result is null)
            return new StatusCodeResult(404);

        return result;
    }

    public async Task<ActionResult<IEnumerable<Category>>> GetCategoriesByName(string name)
    {
        var result = await _readRepository.FindByCondition(x => x.Name == name).ToListAsync();

        if (result is null)
            return new StatusCodeResult(404);

        return result;
    }

    public async Task<ActionResult<Category>> UpdateCategory(Guid id, Category category)
    {
        var result = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

        if (result is null)
            return new StatusCodeResult(404);

        result.Put(category);
        _writeRepository.Update(result);

        await _unitOfWork.CommitAsync();

        return result;
    }
}