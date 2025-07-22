using BS.Domain.Entities;

namespace BS.Application.Interfaces;

public interface IJwtTokenGenerator
{
    string GenerateToken(User user, IList<string> roles);
}