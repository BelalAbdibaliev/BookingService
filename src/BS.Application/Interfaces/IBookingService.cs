using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IBookingService
{
    Task BookAsync(CreateBookingDto dto);
    Task<BookingDto?> FindBookingById(int id);
    Task<List<BookingDto>?> FindBookingByUserId(string id);
    Task DeleteBooking(int id);
}