using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class ImagePost
    {
        [Key]
        public int Id { get; set; }
        public int ImageId { get; set; }
        public Image? Image { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
