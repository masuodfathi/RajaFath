using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
        public class Image
        {
            [Key]
            [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
            public int Id { get; set; }

            [Required]
            public string? FileName { get; set; }
            public string? Caption { get; set; }
            public string? AltText { get; set; }
            public string? ImageUrl { get; set; }
            public DateTime? UploadDate { get; set; }

            // Navigation property for the associated image-post relationships
            public List<ImagePost> ImagePosts { get; set; }
        }
}
