using BS.Domain.Entities;
using BS.Domain.Enums;

namespace BS.Application.Dto;

public class BookingDto
{
    public string UserId { get; set; }
    
    public int SpotId { get; set; }
    
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public BookingStatus Status { get; set; }
}