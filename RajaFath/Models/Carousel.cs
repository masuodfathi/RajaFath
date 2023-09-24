using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class Carousel
    {
        [Key]
        public string Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? Path { get; set; }
    }
}
