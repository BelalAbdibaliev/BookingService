using BS.Domain.Entities;
using BS.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;

namespace BS.Infrastructure.Services;

public static class DataSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
    {
        var roles = new[] { "Admin", "User" };

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
            {
                await roleManager.CreateAsync(new IdentityRole(role));
            }
        }
    }
    
    public static async Task SeedSpot(ApplicationDbContext dbContext)
    {
        if (!dbContext.Resources.Any())
            throw new InvalidOperationException("You have to create resource first!.");

        var resources = dbContext.Resources.ToList();
        foreach (var resource in resources)
        {
            if (dbContext.Spots.Any(s => s.ResourceId == resource.Id))
                continue;

            for (int i = 1; i <= resource.Capacity; i++)
            {
                var spot = resource.AddSpot(i.ToString(), 0);
                dbContext.Spots.Add(spot);
            }
        }

        await dbContext.SaveChangesAsync();
    }
    
    public static async Task SeedResource(ApplicationDbContext dbContext)
    {
        if (dbContext.Resources.Any())
            return;

        var resources = new List<Resource>
        {
            new Resource("Hall A", "Main hall", 50, "floor 1", true),
            new Resource("Hall B", "Smaller hall", 30, "floor 2", true),
            new Resource("Meeting room", "For meetings", 10, "floor 3", true),
            new Resource("VIP-hall", "Private hall", 5, "floor 4", true)
        };

        dbContext.Resources.AddRange(resources);
        await dbContext.SaveChangesAsync();
    }
}