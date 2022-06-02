using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PageAnalyzerNET.Models;

namespace PageAnalyzerNET.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly PageAnalyzerNetContext _db;

    public HomeController(ILogger<HomeController> logger, PageAnalyzerNetContext context)
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
        try
        {
            var fullUrl = new Uri(name);
            var url = new Url(fullUrl.GetLeftPart(UriPartial.Authority));

            var existUrl = await _db.Urls.FirstOrDefaultAsync(u => u.Name == url.Name);
            if (existUrl != null)
            {
                HttpContext.Session.SetString("flash", "Данный url уже существует в системе.");
                HttpContext.Session.SetString("flash-type", "danger");
                return Redirect("/");
            }
            
            _db.Add(url);
            await _db.SaveChangesAsync();
        }
        catch (UriFormatException e)
        {
            HttpContext.Session.SetString("flash", "Некорректный url. Пожалуйста, проверьте введенное значение.");
            HttpContext.Session.SetString("flash-type", "danger");
            return Redirect("/");
        }
        
        HttpContext.Session.SetString("flash", "Url успешно добавлен.");
        HttpContext.Session.SetString("flash-type", "success");
        return Redirect("/urls");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}