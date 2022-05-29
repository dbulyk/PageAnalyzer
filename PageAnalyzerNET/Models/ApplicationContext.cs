using Microsoft.EntityFrameworkCore;

namespace PageAnalyzerNET.Models;

public class ApplicationContext : DbContext
{
    public DbSet<Url> Urls { get; set; } = null!;
    public DbSet<UrlCheck> UrlChecks { get; set; } = null!;
    
    public ApplicationContext(DbContextOptions options)
        : base(options)
    {
    }
    
}