using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly IRegionRepository _regionRepository;

    public RegionsController(IRegionRepository regionRepository)
    {
        _regionRepository = regionRepository;
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var regions = await _regionRepository.GetAllRegionsAsync();
        var regionsDto = new List<RegionDto>();
        foreach (var region in regions)
        {
            regionsDto.Add(new RegionDto()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                RegionImageUrl = region.RegionImageUrl
            });
        }

        return Ok(regionsDto);
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetRegionById([FromRoute] Guid id)
    {
        var region = await _regionRepository.GetRegionByIdAsync(id);

        if (region == null) return NotFound();

        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        };

        return Ok(regionDto);
    }

    [HttpPost]
    public async Task<IActionResult> CreateRegion([FromBody] AddRegionDto request)
    {
        var region = new Region()
        {
            Code = request.Code,
            Name = request.Name,
            RegionImageUrl = request.RegionImageUrl
        };

        region = await _regionRepository.CreateRegionAsync(region);

        var regionDto = new RegionDto()
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        };

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