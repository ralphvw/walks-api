using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateWalkAsync(Walk walk);
    Task<List<Walk>> GetWalksAsync();
    Task<Walk?> GetWalkByIdAsync(Guid id);
    Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
}