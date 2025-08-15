using Microsoft.AspNetCore.Identity;

namespace BS.Domain.Entities;

public class User: IdentityUser
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}