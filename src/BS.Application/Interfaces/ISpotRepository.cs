using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface ISpotRepository
{
    Task<Spot?> GetByIdWithResourceAsync(int id);
}