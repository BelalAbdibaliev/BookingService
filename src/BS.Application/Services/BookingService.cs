using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace BS.Application.Services;

public class BookingService: IBookingService
{
    private readonly IRepository<Booking> _repository;
    private readonly IRepository<Resource> _resourceRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<BookingService> _logger;
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(
        IRepository<Booking> repository,
        ILogger<BookingService> logger,
        IMapper mapper,
        UserManager<User> userManager,
        IRepository<Resource> resourceRepository,
        IBookingRepository bookingRepository)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _resourceRepository = resourceRepository;
        _bookingRepository =  bookingRepository;
    }
    
    public async Task Book(BookingDto dto)
    {
        if(dto is  null)
            throw new ArgumentNullException(nameof(dto));
        
        var user = await _userManager.FindByIdAsync(dto.UserId);
        var resource = await _resourceRepository.GetById(dto.ResourceId);
        
        if(user == null || resource == null)
        {
            _logger.LogError($"User {user.UserName} not found");
            throw new InvalidOperationException(nameof(dto.UserId));
        }
        
        var bookingModel = _mapper.Map<Booking>(dto);
        
        bookingModel.Resource = resource;
        bookingModel.User = user;

        try
        {
            await _repository.Create(bookingModel);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            throw;
        }
    }

    public async Task<BookingDto?> FindBookingById(int id)
    {
        var booking = await _repository.GetById(id);
        
        var bookingDto = _mapper.Map<BookingDto>(booking);
        
        return bookingDto;
    }

    public async Task<List<BookingDto>?> FindBookingByUserId(string id)
    {
        var booking = await _bookingRepository.GetByUserId(id);
        
        return _mapper.Map<List<BookingDto>>(booking);
    }

    public async Task DeleteBooking(int id)
    {
        try
        {
            await _repository.Delete(id);
            await _repository.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
        }
    }
}