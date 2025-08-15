namespace BS.Application.Interfaces;

public interface IUnconfirmedUserCleanup
{
    Task CleanupAsync(CancellationToken cancellationToken);
}