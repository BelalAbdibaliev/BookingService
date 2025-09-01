namespace BS.Application.Dto;

public class SpotDto
{
    public int Id { get; private set; }
    public string Number { get; private set; }
    public decimal Price { get; private set; }
    public bool IsActive { get; set; }
    public int Capacity { get; set; }

    public int ResourceId { get; private set; }
}