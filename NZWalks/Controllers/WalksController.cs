using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;
using NZWalks.Repositories;

namespace NZWalks.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalksController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IWalkRepository _walkRepository;

    public WalksController(IMapper mapper, IWalkRepository walkRepository)
    {
        _mapper = mapper;
        _walkRepository = walkRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateWalk([FromBody] AddWalkDto request)
    {
        var walk = _mapper.Map<Walk>(request);
        await _walkRepository.CreateWalkAsync(walk);
        return Ok(_mapper.Map<WalkDto>(walk));
    }

    [HttpGet]
    public async Task<IActionResult> GetWalks()
    {
        var walks = await _walkRepository.GetWalksAsync();
        return Ok(_mapper.Map<List<WalkDto>>(walks));
    }

    [HttpGet]
    [Route("{id:Guid}")]
    public async Task<IActionResult> GetWalkById([FromRoute] Guid id)
    {
        var walk = await _walkRepository.GetWalkByIdAsync(id);
        if (walk == null) return NotFound();
        return Ok(_mapper.Map<WalkDto>(walk));
    }

    [HttpPut]
    [Route("{id:Guid}")]
    public async Task<IActionResult> UpdateWalk([FromRoute] Guid id, UpdateWalkDto request)
    {
        var walk = _mapper.Map<Walk>(request);
        walk = await _walkRepository.UpdateWalkAsync(id, walk);

        if (walk == null) return NotFound();

        return Ok(_mapper.Map<WalkDto>(walk));
    }

    [HttpDelete]
    [Route("{id:Guid}")]
    public async Task<IActionResult> DeleteWalk([FromRoute] Guid id)
    {
        var walk = await _walkRepository.DeleteWalkAsync(id);
        if (walk == null) return NotFound();

        return Ok(_mapper.Map<WalkDto>(walk));
    }
}