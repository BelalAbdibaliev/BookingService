using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[ApiController]
[Route("api/book")]
public class BookingController: Controller
{
    private readonly IBookingService _bookingService;

    public BookingController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpPost]
    public async Task<IActionResult> Book([FromBody] BookingDto booking)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _bookingService.BookAsync(booking);
        return Ok();
    }

    [HttpGet("getall")]
    public async Task<IActionResult> GetAllBooks([FromQuery] string userId)
    {
        var books = await _bookingService.FindBookingByUserId(userId);
        
        if(books == null)
            return NotFound();
        
        return Ok(books);
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetBook([FromQuery] int id)
    {
        var book = await _bookingService.FindBookingById(id);
        
        if(book == null)
            return NotFound();
        
        return Ok(book);
    }

    [HttpPost("delete")]
    public async Task<IActionResult> DeleteBook([FromQuery] int id)
    {
        await _bookingService.DeleteBooking(id);
        
        return Ok();
    }
}