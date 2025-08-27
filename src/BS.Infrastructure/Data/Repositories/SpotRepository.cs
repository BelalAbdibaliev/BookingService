using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Repositories;

public class SpotRepository: ISpotRepository
{
    private readonly ApplicationDbContext _dbContext;

    public SpotRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Spot?> GetByIdWithResourceAsync(int id)
    {
        return await _dbContext.Spots
            .Include(s => s.Resource)
            .FirstOrDefaultAsync(s => s.Id == id);
    }
}