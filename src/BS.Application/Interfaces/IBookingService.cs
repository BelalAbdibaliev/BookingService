using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IBookingService
{
    Task Book(BookingDto dto);
    Task<BookingDto?> FindBookingById(int id);
    Task<List<BookingDto>?> FindBookingByUserId(string id);
    Task DeleteBooking(int id);
}