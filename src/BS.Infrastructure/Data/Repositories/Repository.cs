using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Repositories;

public class Repository<T>: IRepository<T>
where T : BaseEntity
{
    private readonly ApplicationDbContext _context;

    public Repository(ApplicationDbContext context)
    {
        _context = context;
    }
    
    public async Task<T?> GetById(int id)
    {
        return await _context.Set<T>().FindAsync(id);
    }

    public async Task<IEnumerable<T>?> GetAll()
    {
        return await _context.Set<T>().ToListAsync();
    }

    public async Task Create(T entity)
    {
        await _context.Set<T>().AddAsync(entity);
    }

    public async Task Delete(int id)
    {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity != null)
            _context.Set<T>().Remove(entity);
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}