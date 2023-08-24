using Bernhoeft.Domain.Entities;
using Bernhoeft.Infra.Data;
using Bernhoeft.WebApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bernhoeft.WebApi.Services
{
    public class ProductService : IProductService
    {
        private readonly IWriteRepository<Product> _writeRepository;
        private readonly IReadRepository<Product> _readRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IWriteRepository<Product> writeRepository, IReadRepository<Product> readRepository, IUnitOfWork unitOfWork)
        {
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            return await _readRepository.FindAll().ToListAsync();    
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetActiveProducts(bool active)
        {
            var result = await _readRepository.FindByCondition(x => x.Active == active).ToListAsync();

            if (result is null)
                return new StatusCodeResult(404);

            return result;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetProductsByName(string name)
        {
            var result = await _readRepository.FindByCondition(x => x.Name == name).ToListAsync();

            if (result is null)
                return new StatusCodeResult(404);

            return result;
        }

        public async Task<ActionResult<IEnumerable<Product>>> GetByCategoriesId(Guid categoryId)
        {
            var result = await _readRepository.FindByCondition(x => x.CategoryId == categoryId).ToListAsync();

            if (result is null)
                return new StatusCodeResult(404);

            return result;
        }

        public async Task<ActionResult<Product>> Create(Product product)
        {
            var result = new Product(product.Name, product.Description, product.Price, product.CategoryId);
            await _writeRepository.AddAsync(result);
            await _unitOfWork.CommitAsync();
            return result;
        }

        public async Task<ActionResult<Product>> UpdateProduct(Guid id, Product product)
        {
            var result = await _readRepository.FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

            if (result is null)
                return new StatusCodeResult(404); 

            result.Put(product);

            _writeRepository.Update(result);
            await _unitOfWork.CommitAsync();

            return result;
        }
    }
}