using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Models;

namespace UrlShortener.Services
{
    public class UrlService : IUrlService
    {
        private readonly AppDbContext _context;
        
        public UrlService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<string> CreateShortUrlAsync(string originalUrl, string? customSlug = null)
        {
            var existing = await _context.Urls
                .FirstOrDefaultAsync(u => u.OriginalUrl == originalUrl);

            if (existing != null)
                return existing.ShortCode;

            string shortCode;

            if (!string.IsNullOrWhiteSpace(customSlug))
            {
                if (!await IsShortCodeAvailableAsync(customSlug))
                {
                    throw new InvalidOperationException("Bu özel link zaten kullanımda");
                }

                shortCode = customSlug;
            }
            else
            {
                shortCode = GenerateUniqueShortCode();
            }

            var urlModel = new UrlModel
            {
                OriginalUrl = originalUrl,
                ShortCode = shortCode,
                CreatedDate = DateTime.Now,
                ClickCount = 0
            };

            _context.Urls.Add(urlModel);
            await _context.SaveChangesAsync();

            return shortCode;
        }

        public async Task<bool> IsShortCodeAvailableAsync(string shortCode)
        {
            return !await _context.Urls.AnyAsync(u => u.ShortCode == shortCode);
        }

        public async Task<UrlModel> GetByShortCodeAsync(string shortCode)
        {
            var url = await _context.Urls
                .FirstOrDefaultAsync(u => u.ShortCode == shortCode);

            if (url != null)
            {
                url.ClickCount++;
                await _context.SaveChangesAsync();
            }

            return url;
        }

        public async Task<List<UrlModel>> GetAllUrlsAsync()
        {
            return await _context.Urls
                .OrderByDescending(u => u.CreatedDate)
                .ToListAsync();
        }
        
        private string GenerateUniqueShortCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var random = new Random();
            string shortCode;

            do
            {
                shortCode = new string(Enumerable.Repeat(chars, 6)
                    .Select(s => s[random.Next(s.Length)]).ToArray());
            }
            while (_context.Urls.Any(u => u.ShortCode == shortCode));

            return shortCode;
        }
    }
}