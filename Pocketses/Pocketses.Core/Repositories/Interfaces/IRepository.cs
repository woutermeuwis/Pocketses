using Pocketses.Core.Models.Specifications;

namespace Pocketses.Core.Repositories.Interfaces;

internal interface IRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> GetAllAsync(ISpecification<T> specification);
    Task<T> GetAsync(Guid id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteAsync(Guid id);
}
