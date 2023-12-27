using AutoMapper;
using Microsoft.AspNetCore.Mvc;
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

    public RegionsController(IRegionRepository regionRepository, IMapper mapper)
    {
        _regionRepository = regionRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regions = await _regionRepository.GetAllRegionsAsync();

        return Ok(_mapper.Map<List<RegionDto>>(regions));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetRegionByIdAsync(id);

        if (region == null) return NotFound();

        return Ok(_mapper.Map<RegionDto>(region));
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto request)
    {
        var region = _mapper.Map<Region>(request);

        region = await _regionRepository.CreateRegionAsync(region);

        var regionDto = _mapper.Map<RegionDto>(region);

        return CreatedAtAction(nameof(GetRegionById), new { id = region.Id }, regionDto);
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
    {
        var region = await _regionRepository.UpdateRegionAsync(id, request);

        if (region == null) return NotFound();

        return Ok(region);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteRegion([FromRoute]Guid id)
    {
        var region = await _regionRepository.DeleteRegionAsync(id);

        if (region == null) return NotFound();

        return Ok();
    }
}