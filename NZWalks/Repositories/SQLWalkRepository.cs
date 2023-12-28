using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class SQLWalkRepository: IWalkRepository
{
    private readonly WalksDbContext _dbContext;

    public SQLWalkRepository(WalksDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<Walk> CreateWalkAsync(Walk walk)
    {
        await _dbContext.Walks.AddAsync(walk);
        await _dbContext.SaveChangesAsync();
        return walk;
    }

    public async Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null)
    {
        var walks = _dbContext.Walks.Include("Difficulty").Include("Region").AsQueryable();

        if (string.IsNullOrWhiteSpace(filterOn) != false || string.IsNullOrWhiteSpace(filterQuery) != false)
            return await walks.ToListAsync();
        if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
        {
            walks = walks.Where(x => x.Name.Contains(filterQuery));
        }
        return await walks.ToListAsync();
    }

    public async Task<Walk?> GetWalkByIdAsync(Guid id)
    {
        return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<Walk?> UpdateWalkAsync(Guid id, Walk walk)
    {
        var existingWalk = await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
        if (existingWalk == null) return null;

        existingWalk.Name = walk.Name;
        existingWalk.Description = walk.Description;
        existingWalk.LengthInKm = walk.LengthInKm;
        existingWalk.WalkImageUrl = walk.WalkImageUrl;
        existingWalk.DifficultyId = walk.DifficultyId;
        existingWalk.RegionId = walk.RegionId;

        await _dbContext.SaveChangesAsync();

        return existingWalk;
    }

    public async Task<Walk?> DeleteWalkAsync(Guid id)
    {
        var walk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        if (walk == null) return null;

        _dbContext.Walks.Remove(walk);
        await _dbContext.SaveChangesAsync();

        return walk;
    }
}