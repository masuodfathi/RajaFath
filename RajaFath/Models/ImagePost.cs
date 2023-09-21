namespace RajaFath.Models
{
    public class ImagePost
    {
        public int ImageId { get; set; }
        public Image? Image { get; set; }

        public int PostId { get; set; }
        public Post? Post { get; set; }
    }
}
