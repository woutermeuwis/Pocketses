namespace Pocketses.Core.Repositories.Interfaces;

public interface IRepository<T> where T : class
{
	Task<List<T>> GetAllAsync();
	Task<T> GetAsync(Guid id);
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(Guid id);
}