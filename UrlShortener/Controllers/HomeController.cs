using Microsoft.AspNetCore.Mvc;

namespace UrlShortener.Controllers;

public class HomeController : Controller
{

    public HomeController()
    {
        
    }

    public IActionResult Index()
    {
        return View();
    }

}
