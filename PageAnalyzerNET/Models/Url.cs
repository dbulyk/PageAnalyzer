using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace PageAnalyzerNET.Models;

[Index(nameof(Name), IsUnique=true)]
public class Url
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    [Required]
    [Url]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public List<UrlCheck> UrlChecks { get; set; } = new List<UrlCheck>();

    public Url(string name)
    {
        Name = name;
    }
}