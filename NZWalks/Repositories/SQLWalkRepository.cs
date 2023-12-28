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

    public async Task<List<Walk>> GetWalksAsync()
    {
        var walks = await _dbContext.Walks.Include("Difficulty").Include("Region").ToListAsync();
        return walks;
    }

    public async Task<Walk?> GetWalkByIdAsync(Guid id)
    {
        return await _dbContext.Walks.Include("Difficulty").Include("Region").FirstOrDefaultAsync(x => x.Id == id);
    }
}