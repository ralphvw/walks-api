using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

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
}