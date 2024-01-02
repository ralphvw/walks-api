using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories;

public class LocalImageRepository: IImageRepository
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _contextAccessor;
    private readonly WalksDbContext _dbContext;

    public LocalImageRepository(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor contextAccessor, WalksDbContext dbContext)
    {
        _webHostEnvironment = webHostEnvironment;
        _contextAccessor = contextAccessor;
        _dbContext = dbContext;
    }
    
    public async Task<Image> Upload(Image image)
    {
        var localFilePath = Path.Combine(_webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
        
        using var stream = new FileStream(localFilePath, FileMode.Create);

        await image.File.CopyToAsync(stream);
        
        var urlFilePath =
            $"{_contextAccessor.HttpContext.Request.Scheme}://{_contextAccessor.HttpContext.Request.Host}" +
            $"{_contextAccessor.HttpContext.Request.PathBase}/Images/{image.FileName}{image.FileExtension}";
        
        image.FilePath = urlFilePath;

        await _dbContext.Images.AddAsync(image);
        await _dbContext.SaveChangesAsync();

        return image;
    }
}