using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateWalkAsync(Walk walk);
    Task<List<Walk>> GetWalksAsync();
}