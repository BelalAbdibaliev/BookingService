using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IJwtTokenGenerator
{
    Task<string> GenerateTokenAsync(User user);
}