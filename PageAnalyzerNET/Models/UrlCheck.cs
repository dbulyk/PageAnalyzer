using System.ComponentModel.DataAnnotations;

namespace PageAnalyzerNET.Models;

public class UrlCheck
{
    [Key]
    public int Id { get; set; }
    public int? StatusCode { get; set; }
    
    [MaxLength(50)] 
    public string? Title { get; set; }
    
    [MaxLength(50)]
    public string? H1 { get; set; }
    public string? Description { get; set; }
    public Url Url { get; set; }
    
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

    public UrlCheck()
    {
    }

    public UrlCheck(int? statusCode, Url url)
    {
        StatusCode = statusCode;
        Url = url;
    }

    public UrlCheck(int? statusCode, string? title, string? h1, string? description, Url url)
    {
        StatusCode = statusCode;
        Title = title;
        H1 = h1;
        Description = description;
        Url = url;
    }
}