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
    
    [Timestamp]
    public DateTime CreatedAt { get; set; }
}