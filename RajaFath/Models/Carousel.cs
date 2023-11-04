using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RajaFath.Models
{
    public class Carousel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CarouselId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
        public int? PostId { get; set; }

        // Navigation property to represent the relationship with Post
        public Post? Post { get; set; }

    }
}
