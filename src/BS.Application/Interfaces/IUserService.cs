using BS.Application.Dto;
using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IUserService
{
    Task<string?> LoginAsync(LoginDto  userDto);
    Task<string?> RegisterAsync(RegisterUserDto user);
    Task LogoutAsync();
    Task<bool> ConfirmEmailAsync(string userId, string token);
    Task<bool> ResendConfirmationEmailAsync(string email);
}