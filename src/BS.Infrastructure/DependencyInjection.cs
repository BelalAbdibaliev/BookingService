using System.Text;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using BS.Infrastructure.Data;
using BS.Infrastructure.Data.Repositories;
using BS.Infrastructure.Middlewares;
using BS.Infrastructure.Services;
using BS.Infrastructure.Workers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Sinks.TelegramBot;

namespace BS.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IHostBuilder host)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<User, IdentityRole>(options =>
        {
            options.Password.RequiredLength = 8;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            
            options.User.RequireUniqueEmail = true;
            
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
        })
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();
        
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ClockSkew = TimeSpan.Zero,
                
                    ValidIssuer = configuration["JwtSettings:Issuer"],
                    ValidAudience = configuration["JwtSettings:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(configuration["JwtSettings:SecretKey"]))
                };
            });
        
        services.AddAuthorization(options =>
        {
            options.AddPolicy("RequireAdminRole", 
                policy => policy.RequireRole("Admin"));
            options.AddPolicy("RequireModeratorRole", 
                policy => policy.RequireRole("Moderator"));
        });

        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IAuthService, AuthService>();
        services.AddTransient<IEmailSender>(sp =>
            new SmtpEmailSender(
                smtpServer: "smtp.gmail.com",
                smtpPort: 587,
                fromEmail: "",
                password: ""
            )
        );
        services.AddScoped<IUnconfirmedUserCleanup, UnconfirmedUserCleanup>();
        services.AddHostedService<UnconfirmedUserCleanupWorker>();
        services.AddScoped<IBookingRepository, BookingRepository>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        services.AddProblemDetails();
        
        host.UseSerilog((ctx, lc) =>
        {
            lc.WriteTo.File("../../logs/log-.txt", rollingInterval: RollingInterval.Day)
                .WriteTo.Console()
                .WriteTo.TelegramBot(
                    token: configuration["Logging:Telegram:BotToken"],
                    chatId: configuration["Logging:Telegram:ChatId"],
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                );
        });
        
        return services;
    }
}