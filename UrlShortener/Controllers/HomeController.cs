using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Services;

namespace UrlShortener.Controllers;

public class HomeController : Controller
{
    private readonly IUrlService _urlService;

    public HomeController(IUrlService urlService)
    {
        _urlService = urlService;
    }

    public async Task<IActionResult> Index()
    {
        var urls = await _urlService.GetAllUrlsAsync();
        return View(urls);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> ShortenUrl(ShortenUrlRequest request)
    {
        if (!ModelState.IsValid)
        {
            var urls = await _urlService.GetAllUrlsAsync();
            ViewData["Errors"] = ModelState;
            return View("Index", urls);
        }

        try
        {
            var shortCode = await _urlService.CreateShortUrlAsync(request.OriginalUrl);
            var shortUrl = Url.Action("Redirect", "Home", new { code = shortCode }, Request.Scheme);

            TempData["Success"] = $"Kısaltılmış URL: {shortUrl}";
        }
        catch (Exception ex)
        {
            TempData["Error"] = "Hata: " + ex.Message;
        }

        return RedirectToAction("Index");
    }

    [HttpGet]
    [Route("Home/Redirect")]
    public async Task<IActionResult> RedirectToUrl(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            return BadRequest();
        
        var url = await _urlService.GetByShortCodeAsync(code);
        
        if (url == null || url.OriginalUrl == null)
            return NotFound("URL bulunamadı!");
        
        string destinationUrl = url.OriginalUrl;
        
        // Check if original URL starts with http:// or https://
        if (!destinationUrl.StartsWith("http://", StringComparison.OrdinalIgnoreCase) && 
            !destinationUrl.StartsWith("https://", StringComparison.OrdinalIgnoreCase))
        {
            destinationUrl = "https://" + destinationUrl;
        }
        
        return Redirect(destinationUrl);
    }

}
