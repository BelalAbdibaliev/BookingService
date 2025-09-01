using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface ISpotService
{
    Task<List<SpotDto>> GetAllSpots();
    Task<SpotDto?> GetSpotById(int id);
}