using System.ComponentModel.DataAnnotations;

namespace NZWalks.Models.DTO;

public class AddRegionDto
{
    [Required]
    [MinLength(3, ErrorMessage ="Code is a minimum of three characters")]
    [MaxLength(3, ErrorMessage ="Code is a maximum of three characters")]
    public string Code { get; set; }
    [Required]
    public string Name { get; set; }
    public string? RegionImageUrl { get; set; }
}