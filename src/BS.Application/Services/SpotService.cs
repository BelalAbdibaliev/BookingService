using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;

namespace BS.Application.Services;

public class SpotService:ISpotService
{
    private readonly IRepository<Spot> _repository;
    private readonly IMapper _mapper;

    public SpotService(IRepository<Spot> repository,  IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    public async Task<List<SpotDto>> GetAllSpots()
    {
        var spots = await _repository.GetAll();
        return _mapper.Map<List<SpotDto>>(spots);
    }

    public async Task<SpotDto?> GetSpotById(int id)
    {
        var spot = await _repository.GetById(id);
        return _mapper.Map<SpotDto>(spot);
    }
}