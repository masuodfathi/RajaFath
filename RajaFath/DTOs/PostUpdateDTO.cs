using Microsoft.AspNetCore.Identity;
using RajaFath.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RajaFath.DTOs
{
    public class PostUpdateDTO
    {
        public int? Id { get; set; }

        [Required(ErrorMessage = "عنوان نمی‌تواند خالی باشد.")]
        [MaxLength(100)]
        public string? Title { get; set; }
        [AllowHtml]
        [MaxLength(50000)]
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public string? AuthorId { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        public List<Category>? Categories { get; set; }
        public List<SelectListItem>? AvailableCategories { get; set; }
        public IdentityUser? CurrentUser { get; set; }
        [Display(Name = "Categories")]
        public List<int>? SelectedCategoryIds { get; set; }
        public int? FeatureImageId { get; set; }
        public Image? FeatureImage { get; set; }
        public string? PostType { get; set; }
    }
}
