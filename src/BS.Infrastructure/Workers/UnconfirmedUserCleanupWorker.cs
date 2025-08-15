using BS.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BS.Infrastructure.Workers;

public class UnconfirmedUserCleanupWorker: BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly TimeSpan _checkInterval = TimeSpan.FromHours(1);

    
    public UnconfirmedUserCleanupWorker(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var cleanupService = scope.ServiceProvider.GetRequiredService<IUnconfirmedUserCleanup>();
            
            await cleanupService.CleanupAsync(stoppingToken);
            await Task.Delay(_checkInterval, stoppingToken);
        }
    }

}