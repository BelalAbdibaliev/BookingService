using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IRepository<T>
where T : class
{
    public Task<T?> GetById(int id);
    public Task<IEnumerable<T>?> GetAll();
    public Task Create(T entity);
    public Task Delete(int id);
    public Task<int> SaveChangesAsync();
}