using Microsoft.AspNetCore.Mvc;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RegionsController : ControllerBase
{
    private readonly WalksDbContext _dbContext;

    public RegionsController(WalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    [HttpGet]
    public IActionResult GetAll()
    {
        var regions = _dbContext.Regions.ToList();
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
}