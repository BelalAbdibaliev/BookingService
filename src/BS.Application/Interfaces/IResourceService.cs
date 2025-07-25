using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IResourceService
{
    Task<Resource?> FindAsync(int id);
    Task<List<Resource>?> GetAllAsync();
    Task CreateAsync(CreateResourceDto dto);
    Task UpdateAsync(UpdateResourceDto dto);
    Task DeleteAsync(int id);
}