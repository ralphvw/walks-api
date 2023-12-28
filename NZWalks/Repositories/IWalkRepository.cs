using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public interface IWalkRepository
{
    Task<Walk> CreateWalkAsync(Walk walk);
    Task<List<Walk>> GetWalksAsync(string? filterOn = null, string? filterQuery = null, string? sortBy = null, bool isAscending = true);
    Task<Walk?> GetWalkByIdAsync(Guid id);
    Task<Walk?> UpdateWalkAsync(Guid id, Walk walk);
    Task<Walk?> DeleteWalkAsync(Guid id);
}