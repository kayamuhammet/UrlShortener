using QRCoder;

namespace UrlShortener.Services
{
    public class QrCodeService : IQrCodeService
    {
        public string GenerateQrCodeDataUrl(string text)
        {
            var qrBytes = GenerateQrCodeBytes(text);
            return $"data:image/png;base64,{Convert.ToBase64String(qrBytes)}";
        }

        public byte[] GenerateQrCodeBytes(string text)
        {
            using var qrGenerator = new QRCodeGenerator();
            using var qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            using var qrCode = new PngByteQRCode(qrCodeData);

            return qrCode.GetGraphic(20);

        }
    }
}