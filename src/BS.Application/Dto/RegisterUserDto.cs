using System.ComponentModel.DataAnnotations;

namespace BS.Application.Dto;

public class RegisterUserDto
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    [Compare("Password")]
    public string ConfirmPassword { get; set; }
    [Required]
    public string UserName { get; set; }
}