using AutoMapper;
using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

public class BookingService : IBookingService
{
    private readonly IRepository<Booking> _repository;
    private readonly IRepository<Spot> _spotGenericRepository;
    private readonly ISpotRepository _spotRepository;
    private readonly UserManager<User> _userManager;
    private readonly ILogger<BookingService> _logger;
    private readonly IMapper _mapper;
    private readonly IBookingRepository _bookingRepository;

    public BookingService(
        IRepository<Booking> repository,
        ILogger<BookingService> logger,
        IMapper mapper,
        UserManager<User> userManager,
        IRepository<Spot> spotGenericRepository,
        IBookingRepository bookingRepository,
        ISpotRepository spotRepository)
    {
        _repository = repository;
        _logger = logger;
        _mapper = mapper;
        _userManager = userManager;
        _spotGenericRepository = spotGenericRepository;
        _bookingRepository = bookingRepository;
        _spotRepository = spotRepository;
    }

    public async Task BookAsync(BookingDto dto)
    {
        if (dto is null)
            throw new ArgumentNullException(nameof(dto));

        var user = await _userManager.FindByIdAsync(dto.UserId);
        var spot = await _spotRepository
            .GetByIdWithResourceAsync(dto.SpotId);

        if (user == null || spot == null)
        {
            _logger.LogError("User {UserId} or Spot {SpotId} not found", dto.UserId, dto.SpotId);
            throw new InvalidOperationException("User or Spot not found");
        }

        var alreadyBooked = await _bookingRepository.ExistsActiveBookingAsync(spot.Id);
        if (alreadyBooked)
            throw new InvalidOperationException("This place is already booked.");

        var booking = Booking.Create(spot, user.Id, dto.StartTime, dto.EndTime);
        
        spot.IsActive = false;

        await _repository.Create(booking);
        await _repository.SaveChangesAsync();
    }

    public async Task<BookingDto?> FindBookingById(int id)
    {
        var booking = await _repository.GetById(id);
        if (booking is null)
            return null;
        
        return _mapper.Map<BookingDto>(booking);
    }

    public async Task<List<BookingDto>?> FindBookingByUserId(string id)
    {
        var booking = await _bookingRepository.GetByUserId(id);
        if (booking is null)
            return null;
        
        return _mapper.Map<List<BookingDto>>(booking);
    }

    public async Task DeleteBooking(int id)
    {
        var booking = await _repository.GetById(id);
        if (booking is null)
            throw new NullReferenceException("Booking not found");
        
        await _repository.Delete(id);
        await _repository.SaveChangesAsync();
    }
}
