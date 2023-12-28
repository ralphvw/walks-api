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
}