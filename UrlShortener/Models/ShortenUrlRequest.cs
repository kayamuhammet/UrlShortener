using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class ShortenUrlRequest
    {
        [Required(ErrorMessage = "URL gereklidir")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Kısaltmak istediğiniz URL")]
        public string? OriginalUrl { get; set; }

        [RegularExpression(@"^[a-zA-Z0-9-]*$", ErrorMessage = "Özel link sadece harf, rakam ve tire (-) içerebilir")]
        [StringLength(50, ErrorMessage = "Özel link en fazla 50 karakter olabilir")]
        [Display(Name = "Özel Kısa Link (İsteğe Bağlı)")]
        public string? CustomSlug { get; set; }
    }
}