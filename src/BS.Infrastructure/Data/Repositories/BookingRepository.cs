using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BS.Infrastructure.Data.Repositories;

public class BookingRepository:  IBookingRepository
{
    private readonly ApplicationDbContext _context;
    
    public BookingRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<List<Booking>> GetByUserId(string id)
    {
        return await _context.Bookings
            .Where(u => u.UserId == id)
            .ToListAsync();
    }
}