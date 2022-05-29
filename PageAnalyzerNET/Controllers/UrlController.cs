using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageAnalyzerNET.Models;
using PageAnalyzerNET.ViewModels;

namespace PageAnalyzerNET.Controllers;

public class UrlController : Controller
{
    private readonly ApplicationContext _db;
    
    public UrlController(ApplicationContext context)
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

    // [HttpPost]
    // public async Task<IActionResult> Show(Url url)
    // {
    //     
    // }


}