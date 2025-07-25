namespace BS.Application.Dto;

public class UpdateResourceDto
{
    public int Id { get; set; }
    
    public string? Name { get; set; }
    public string? Description { get; set; }
    public int? Capacity { get; set; }
    public string? Location { get; set; }
    public bool? IsActive { get; set; }
}