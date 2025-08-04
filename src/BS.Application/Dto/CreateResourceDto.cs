using System.ComponentModel.DataAnnotations;

namespace BS.Application.Dto;

public class CreateResourceDto
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string Description { get; set; }
    [Required]
    public int Capacity { get; set; }
    [Required]
    public string Location { get; set; }
    [Required]
    public bool IsActive { get; set; }
}