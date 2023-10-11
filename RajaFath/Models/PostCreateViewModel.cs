using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace RajaFath.Models
{
    public class PostCreateViewModel
    {
        public int? PostId { get; set; }
        
        [Required(ErrorMessage = "عنوان نمی‌تواند خالی باشد.")]
        [MaxLength(100)]
        public string Title { get; set; }
        [AllowHtml]
        [MaxLength(50000)]
        public string? Content { get; set; }
        public string? Excerpt { get; set; }
        public string? Author { get; set; }
        public DateTime? UpdateDate { get; set; }    
        public DateTime? CreatedDate { get;set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }

        [Display(Name = "Tags")]
        public List<Tag>? Tags { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? SelectedTag { get; set; }
        public string? NewTag { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? AvailableTags { get; set; }

        public List<Category>? Categories { get; set; }
        public List<Microsoft.AspNetCore.Mvc.Rendering.SelectListItem>? AvailableCategories { get; set; }
        public IdentityUser? CurrentUser { get; internal set; }
        [Display(Name = "Categories")]
        public List<int>? SelectedCategoryIds { get; set; }
        public int? FeatureImageId { get; set; }
        public Image? FeatureImage { get; set; }
        public List<Image>? Images { get; set; }
    }
}
