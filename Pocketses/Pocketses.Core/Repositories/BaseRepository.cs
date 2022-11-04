using Microsoft.EntityFrameworkCore;
using Pocketses.Core.DataAccessLayer;
using Pocketses.Core.Extensions;
using Pocketses.Core.Models.Specifications;

namespace Pocketses.Core.Repositories;
public class BaseRepository<T> where T : class
{
    protected PocketsesContext Context { get; set; }
    protected DbSet<T> Table { get; }

    public BaseRepository(PocketsesContext ctx)
    {
        Table = ctx.Set<T>();
        Context = ctx;
    }

    public async Task<T> CreateAsync(T entity)
    {
        var added = await Table.AddAsync(entity);
        await SaveChangesAsync();
        return added.Entity;
    }

    public Task<T> GetAsync(Guid id)
    {
        return Table.FindAsync(id).AsTask();
    }

    public Task<List<T>> GetAllAsync()
    {
        return Table.ToListAsync();
    }

    public Task<List<T>> GetAllAsync(ISpecification<T> specification)
    {
        return Table
            .AsQueryable()
            .EvaluateSpecification(specification)
            .ToListAsync();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        var updated = Table.Update(entity);
        await SaveChangesAsync();
        return updated.Entity;
    }

    public async Task DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        Table.Remove(entity);
        await SaveChangesAsync();
    }

    protected Task SaveChangesAsync() => Context.SaveChangesAsync();
}
