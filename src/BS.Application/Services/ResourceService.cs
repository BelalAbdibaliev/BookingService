using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;

namespace BS.Application.Services;

public class ResourceService: IResourceService
{
    private readonly IRepository<Resource> _repository;
    private readonly IMapper _mapper;

    public ResourceService(
        IRepository<Resource> repository,
        IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Resource?> FindAsync(int id)
    {
        var resource = await _repository.GetById(id);

        return resource;
    }

    public async Task<List<Resource>?> GetAllAsync()
    {
        var resources = await _repository.GetAll();

        return resources?.ToList();
    }

    public async Task CreateAsync(CreateResourceDto dto)
    {
        var resource = _mapper.Map<Resource>(dto);
        
        await _repository.Create(resource);
        await _repository.SaveChangesAsync();
    }

    public async Task UpdateAsync(UpdateResourceDto dto)
    {
        var resource = await _repository.GetById(dto.Id);
        if(resource is null)
            throw new KeyNotFoundException("Resource not found");
        
        if(!string.IsNullOrEmpty(dto.Name))
            resource.Name = dto.Name;
        
        if(!string.IsNullOrEmpty(dto.Description))
            resource.Description = dto.Description;
        
        if(dto.Capacity > 0 && dto.Capacity.HasValue)
            resource.Capacity = dto.Capacity.Value;
        
        if(!string.IsNullOrEmpty(dto.Location))
            resource.Location = dto.Location;
        
        if(dto.IsActive.HasValue)
            resource.IsActive = dto.IsActive.Value;
        
        await _repository.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.Delete(id);
        await _repository.SaveChangesAsync();
    }
}