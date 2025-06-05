namespace UrlShortener.Services
{
    public interface IQrCodeService
    {
        string GenerateQrCodeDataUrl(string text);
        byte[] GenerateQrCodeBytes(string text);
    }
}

