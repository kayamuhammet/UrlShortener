using UrlShortener.Models;

namespace UrlShortener.Services
{
    public interface IUrlService
    {
        Task<string> CreateShortUrlAsync(string originalUrl);
        Task<UrlModel> GetByShortCodeAsync(string shortCode);
        Task<List<UrlModel>> GetAllUrlsAsync();
    }
}