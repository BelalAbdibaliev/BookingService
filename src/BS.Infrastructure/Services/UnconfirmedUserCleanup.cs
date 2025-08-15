using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace BS.Infrastructure.Services;

public class UnconfirmedUserCleanup: IUnconfirmedUserCleanup
{
    private readonly UserManager<User> _userManager;

    public UnconfirmedUserCleanup(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task CleanupAsync(CancellationToken cancellationToken)
    {
        var users = _userManager.Users
            .Where(u => !u.EmailConfirmed && u.CreatedAt < DateTime.UtcNow.AddDays(-3))
            .ToList();

        foreach (var user in users)
            await _userManager.DeleteAsync(user);
    }

}