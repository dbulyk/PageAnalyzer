using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageAnalyzerNET.Models;
using PageAnalyzerNET.ViewModels;

namespace PageAnalyzerNET.Controllers;

public class UrlController : Controller
{
    private readonly PageAnalyzerNetContext _db;
    
    public UrlController(PageAnalyzerNetContext context)
    {
        _db = context;
    }
    
    
    [HttpGet]
    [Route("/urls")]
    public async Task<IActionResult> Index(int page = 1)
    { 
        const int pageSize = 10;
        var urls = _db.Urls.Include(u => u.UrlChecks).AsNoTracking();
        return View(await PaginatedListViewModel<Url>.CreateAsync(urls, page, pageSize));
    }
    
    public async Task<IActionResult> Show(int id)
    {
        var url = await _db.Urls.Include(u => u.UrlChecks).FirstOrDefaultAsync(u => u.Id == id);
        if (url != null)
        {
            return View(url);
        }

        return NotFound();
    }

    [HttpPost]
    public async Task<IActionResult> CheckUrl(int id)
    {
        var url = await _db.Urls.FirstOrDefaultAsync(u => u.Id == id);
        if (url == null)
        {
            return NotFound();
        }
        
        using var client = new HttpClient();
        var result = await client.GetAsync(url.Name);
        if (result.StatusCode != HttpStatusCode.OK)
        {
            var urlCheck = new UrlCheck((int) result.StatusCode, url);
            _db.Add(urlCheck);
        }
        else
        {
            var html = await client.GetStringAsync(url.Name);
            string? title = null;
            string? h1 = null;
            string? desc = null;
            
            if (html.Contains("<title>"))
            {
                title = html[(html.IndexOf("<title>") + 7)..];
                title = title.Remove(title.IndexOf("</title>")).Trim();
            }

            if (html.Contains("<h1"))
            {
                h1 = html[(html.IndexOf("<h1") + 3)..];
                h1 = h1[(h1.IndexOf(">") + 1)..];
                h1 = h1.Remove(h1.IndexOf("</h1>")).Trim();
            }
        
            if (html.Contains("description"))
            {
                desc = html[(html.IndexOf("\"description\" content=\"") + 23)..];
                desc = desc.Remove(desc.IndexOf("\"")).Trim();
            }

            var check = new UrlCheck((int)result.StatusCode, title, h1, desc, url);
            _db.Add(check);
        }

        await _db.SaveChangesAsync();
        return Redirect($"/url/show/{id}");
    }


}