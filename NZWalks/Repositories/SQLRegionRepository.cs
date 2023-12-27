using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories;

public class SQLRegionRepository: IRegionRepository
{
    private readonly WalksDbContext _dbContext;

    public SQLRegionRepository(WalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<List<Region>> GetAllRegionsAsync()
    {
        return await _dbContext.Regions.ToListAsync();
    }

    public async Task<Region?> GetRegionByIdAsync(Guid id)
    {
        return await _dbContext.Regions.FindAsync(id);
    }

    public async Task<Region> CreateRegionAsync(Region request)
    {
        await _dbContext.Regions.AddAsync(request);
        await _dbContext.SaveChangesAsync();
        return request;
    }

    public async Task<Region?> UpdateRegionAsync(Guid id, UpdateRegionDto request)
    {
        var region = await _dbContext.Regions.FindAsync(id);

        if (region == null) return null;
        
        region.Code = request.Code;
        region.Name = request.Name;
        region.RegionImageUrl = request.RegionImageUrl;

        await _dbContext.SaveChangesAsync();

        return region;
    }

    public async Task<Region?> DeleteRegionAsync(Guid id)
    {
        var region = await _dbContext.Regions.FindAsync(id);

        if (region == null) return null;

        _dbContext.Regions.Remove(region);
        await _dbContext.SaveChangesAsync();
        return region;
    }
}