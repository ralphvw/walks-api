using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.Data;

public class WalksAuthDbContext: IdentityDbContext
{
    public WalksAuthDbContext(DbContextOptions<WalksAuthDbContext> options) : base(options)
    {
        
    }
}