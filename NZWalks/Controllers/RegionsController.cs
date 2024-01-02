using System.Text.Json;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NZWalks.CustomActionFilters;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;
    private readonly IMapper _mapper;
    private readonly ILogger<RegionsController> _logger;

    public RegionsController(IRegionRepository regionRepository, IMapper mapper, ILogger<RegionsController> logger)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
        _logger = logger;
    }
    
    [HttpGet]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetAll()
    {
        _logger.LogInformation("Get All Regions Action Method was invoked");
        var regions = await _regionRepository.GetAllRegionsAsync();
        _logger.LogInformation($"Finished fetching: {JsonSerializer.Serialize(regions)}");

        return Ok(_mapper.Map<List<RegionDto>>(regions));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Reader")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetRegionByIdAsync(id);

        if (region == null) return NotFound();

        return Ok(_mapper.Map<RegionDto>(region));
    }

    [HttpPost]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto request)
    {
        
        var region = _mapper.Map<Region>(request);

        region = await _regionRepository.CreateRegionAsync(region);

        var regionDto = _mapper.Map<RegionDto>(region);

        return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    [ValidateModel]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
    {
        var region = await _regionRepository.UpdateRegionAsync(id, request);

        if (region == null) return NotFound();

        return Ok(region);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    [Authorize(Roles = "Writer")]
    public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
    {
        var region = await _regionRepository.DeleteRegionAsync(id);

        if (region == null) return NotFound();

        return Ok();
    }
}