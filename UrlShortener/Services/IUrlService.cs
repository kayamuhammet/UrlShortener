using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<string> CreateShortUrlAsync(string originalUrl, string? customSlug);
        Task<UrlModel> GetByShortCodeAsync(string shortCode);
        Task<List<UrlModel>> GetAllUrlsAsync();
        Task<bool> IsShortCodeAvailableAsync(string shortCode);
    }
}