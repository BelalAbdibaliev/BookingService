using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BS.Infrastructure.Services;

public class AuthService: IAuthService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    public AuthService(UserManager<User> userManager, SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return null;

        var result = await _signInManager.PasswordSignInAsync(user, password, false, false);
        return result.Succeeded? user: null;
    }

    public async Task<User?> RegisterAsync(User user, string password)
    {

        var result = await _userManager.CreateAsync(user, password);
        await _userManager.AddToRoleAsync(user, "User");
        
        return result.Succeeded? user: null;
    }

    public Task LogoutAsync() => _signInManager.SignOutAsync();
}