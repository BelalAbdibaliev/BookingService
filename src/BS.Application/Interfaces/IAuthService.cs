using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(string email, string password);
    Task<User?> RegisterAsync(User user, string password);
    Task LogoutAsync();
}