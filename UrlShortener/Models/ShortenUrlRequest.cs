using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortenUrlRequest
    {
        [Required(ErrorMessage = "URL gereklidir")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Kısaltmak istediğiniz URL")]
        public string? OriginalUrl { get; set; }
    }
}