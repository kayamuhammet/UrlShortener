using System.ComponentModel.DataAnnotations;

namespace UrlShortener.Models
{
    public class UrlModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "URL gereklidir")]
        [Url(ErrorMessage = "Geçerli bir URL giriniz")]
        [Display(Name = "Orijinal URL")]
        public string? OriginalUrl { get; set; }
        
        public string? ShortCode { get; set; }
        
        [Display(Name = "Tarih")]
        public DateTime CreatedDate { get; set; }
        
        [Display(Name = "Tıklanma")]
        public int ClickCount { get; set; }
    }
}