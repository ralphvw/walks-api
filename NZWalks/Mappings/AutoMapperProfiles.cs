using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings;

public class AutoMapperProfiles: Profile
{
    public AutoMapperProfiles()
    {
        CreateMap<Region, RegionDto>().ReverseMap();
        CreateMap<AddRegionDto, RegionDto>().ReverseMap();
        CreateMap<AddWalkDto, Walk>().ReverseMap();
        CreateMap<Walk, WalkDto>().ReverseMap();
        CreateMap<DifficultyDto, DifficultyDto>().ReverseMap();
    }
}