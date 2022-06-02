using Microsoft.EntityFrameworkCore;

namespace PageAnalyzerNET.Models;

public class PageAnalyzerNetContext : DbContext
{
    public DbSet<Url> Urls { get; set; } = null!;
    public DbSet<UrlCheck> UrlChecks { get; set; } = null!;
    
    public PageAnalyzerNetContext(DbContextOptions options)
        : base(options)
    {
    }
    
}