using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IBookingRepository
{
    Task<List<Booking>?> GetByUserId(string id);
    Task<bool> ExistsActiveBookingAsync(int spotId);

}