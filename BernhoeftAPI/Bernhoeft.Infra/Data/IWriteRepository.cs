﻿namespace Bernhoeft.Infra.Data
{
    public interface IWriteRepository<T> where T : class
    {
        void Add(T entity);
        Task<T> AddAsync(T entity);
        void Delete(T entity);
        void Update(T entity);
        void DeleteById(Guid id);
        Task ExcluirAsync(int id);
    }
}