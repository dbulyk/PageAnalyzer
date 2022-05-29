using System.ComponentModel.DataAnnotations;

namespace PageAnalyzerNET.Models;


public class Url
{
    [Key]
    public int Id { get; set; }
    
    [MaxLength(50)]
    [Required]
    [Url]
    public string Name { get; set; }
    
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public List<UrlCheck> UrlChecks { get; set; }

    public Url(string name)
    {
        Name = name;
    }
}