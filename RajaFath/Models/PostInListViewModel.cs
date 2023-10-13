using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class PostInListViewModel
    {
        public int? Id { get; set; }
        [Display(Name = "عنوان")]
        public string? Title { get; set; }
        [Display(Name = "خلاصه")]
        public string? Excerpt { get; set; }
        [Display(Name = "نویسنده")]
        public string? AuthorName { get; set; }
        [Display(Name = "آخرین به‌روز رسانی")]
        public DateTime? UpdateDate { get; set; }

    }
}
