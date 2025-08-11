using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;

namespace BS.Application.Services;

public class UserService: IUserService
{
    private readonly IAuthService _authService;
    private readonly IMapper _mapper;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;

    public UserService(
        IAuthService authService,
        IMapper mapper,
        IJwtTokenGenerator jwtTokenGenerator)
    {
        _authService = authService;
        _mapper = mapper;
        _jwtTokenGenerator = jwtTokenGenerator;
    }
    
    public async Task<string?> LoginAsync(LoginDto  userDto)
    {
        var user = await _authService.LoginAsync(userDto.Email, userDto.Password);
        
        if (user == null) return null;
        
        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
        
        return token;
    }

    public async Task<string?> RegisterAsync(RegisterUserDto userDto)
    {
        var user = _mapper.Map<RegisterUserDto, User>(userDto);
        
        var result = await _authService.RegisterAsync(user, userDto.Password);
        if(result == null) return null;
        
        var token = await _jwtTokenGenerator.GenerateTokenAsync(user);
        return token;
    }

    public async Task LogoutAsync()
    {
        await _authService.LogoutAsync();
    }
}