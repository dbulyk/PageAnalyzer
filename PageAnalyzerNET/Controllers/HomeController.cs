using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using PageAnalyzerNET.Models;

namespace PageAnalyzerNET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ApplicationContext _db;

    public HomeController(ILogger<HomeController> logger, ApplicationContext context)
    {
        _logger = logger;
        _db = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(string name)
    {
        var fullUrl = new Uri(name);
        var url = new Url(fullUrl.GetLeftPart(UriPartial.Authority));
        _db.Add(url);
        await _db.SaveChangesAsync();
        
        _logger.Log(LogLevel.Information, "Пользователь создан", name);
        return Redirect("/urls");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}