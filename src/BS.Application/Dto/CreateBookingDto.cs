namespace BS.Application.Dto;

public class CreateBookingDto
{
    public string UserId { get; set; }
    public int SpotId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}