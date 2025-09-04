using BS.Domain.Entities;

namespace BS.Application.Dto;

public class BookingDto
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public int SpotId { get; set; }
    public DateTime CreatedAt { get; set; }
    public BookingStatus Status { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}