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
    private readonly WalksDbContext _dbContext;
    private readonly IRegionRepository _regionRepository;

    public RegionsController(WalksDbContext dbContext, IRegionRepository regionRepository)
    {
        _dbContext = dbContext;
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
    public IActionResult GetRegionById([FromRoute] Guid id)
    {
        var region = _dbContext.Regions.Find(id);

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
    public IActionResult CreateRegion([FromBody] AddRegionDto request)
    {
        var region = new Region()
        {
            Code = request.Code,
            Name = request.Name,
            RegionImageUrl = request.RegionImageUrl
        };
        _dbContext.Regions.Add(region);
        _dbContext.SaveChanges();

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
    public IActionResult UpdateRegion([FromRoute] Guid id, [FromBody] UpdateRegionDto request)
    {
        var region = _dbContext.Regions.Find(id);

        if (region == null) return NotFound();

        region.Code = request.Code;
        region.Name = request.Name;
        region.RegionImageUrl = request.RegionImageUrl;

        _dbContext.SaveChanges();

        var regionDto = new RegionDto
        {
            Id = region.Id,
            Name = region.Name,
            Code = region.Code,
            RegionImageUrl = region.RegionImageUrl
        };

        return Ok(regionDto);
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public IActionResult DeleteRegion([FromRoute]Guid id)
    {
        var region = _dbContext.Regions.Find(id);

        if (region == null) return NotFound();

        _dbContext.Regions.Remove(region);
        _dbContext.SaveChanges();

        return Ok();
    }
}