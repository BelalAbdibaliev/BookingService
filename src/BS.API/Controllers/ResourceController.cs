using BS.Application.Dto;
using BS.Application.Interfaces;
using BS.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BS.API.Controllers;

[Controller]
[Route("api/resources")]
public class ResourceController: Controller
{
    private readonly IResourceService _resourceService;

    public ResourceController(IResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet("get")]
    public async Task<IActionResult> Get([FromQuery]int id)
    {
        var resource = await _resourceService.FindAsync(id);
        
        if(resource != null)
            return Ok(resource);
        
        return NotFound();
    }

    [HttpGet("getalll")]
    public async Task<IActionResult> GetAll()
    {
        var resources = await _resourceService.GetAllAsync();
        if (resources != null)
            return Ok(resources);
        
        return NotFound();
    }

    [Authorize]
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateResourceDto resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _resourceService.CreateAsync(resource);
        
        return Ok(resource);
    }

    //[Authorize]
    [HttpPatch("update")]
    public async Task<IActionResult> Update([FromBody] UpdateResourceDto resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        await _resourceService.UpdateAsync(resource);
        
        return Ok(resource);
    }

    //[Authorize]
    [HttpPost("delete")]
    public async Task<IActionResult> Delete([FromQuery] int id)
    {
        await _resourceService.DeleteAsync(id);
        
        return Ok();
    }
    
}