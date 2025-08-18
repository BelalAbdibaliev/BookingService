using BS.Application.Interfaces;
using BS.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BS.Application.Configurations;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(cfg => {
            cfg.AddProfile<AutoMappingProfile>();
        });

        services.AddScoped<IResourceService, ResourceService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IBookingService, BookingService>();
        
        return services;
    }
}