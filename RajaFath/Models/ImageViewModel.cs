using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class ImageViewModel
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "Please select an image to upload.")]
        [Display(Name = "Image File")]
        public IFormFile ImageFile { get; set; }

        [Display(Name = "Caption")]
        public string Caption { get; set; }

        [Display(Name = "Alt Text")]
        public string AltText { get; set; }

        [Display(Name = "File Name")]
        public string FileName { get; set; }
    }
}
