using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class PostCreateViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(50000)]
        public string Content { get; set; }


        public string Excerpt { get; set; }
        public string Author { get; set; }
        public DateTime UpdateDate { get; set; }

        // Add other properties like excerpt, author, updated date, etc.

        [Display(Name = "Tags")]
        public List<int> SelectedTagIds { get; set; }

        [Display(Name = "Categories")]
        public List<int> SelectedCategoryIds { get; set; }

        [Display(Name = "Image")]
        public IFormFile ImageFile { get; set; }

        public List<Tag> Tags { get; set; }
        public List<Category> Categories { get; set; }
    }
}
