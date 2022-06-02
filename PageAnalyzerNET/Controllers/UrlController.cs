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
        var urls = _db.Urls.AsNoTracking();
        return View(await PaginatedListViewModel<Url>.CreateAsync(urls, page, pageSize));
    }
    
    public async Task<IActionResult> Show(int id)
    {
        var url = await _db.Urls.FirstOrDefaultAsync(u => u.Id == id);
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
            
        }

        await _db.SaveChangesAsync();
        return Redirect($"/urls/{id}");
    }


}