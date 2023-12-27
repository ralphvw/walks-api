using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Repositories;

public interface IRegionRepository
{
    Task<List<Region>>  GetAllRegionsAsync();
    Task<Region?> GetRegionByIdAsync(Guid id);
    Task<Region> CreateRegionAsync(Region request);
    Task<Region?> UpdateRegionAsync(Guid id, UpdateRegionDto request);
    Task<Region?> DeleteRegionAsync(Guid id);
}