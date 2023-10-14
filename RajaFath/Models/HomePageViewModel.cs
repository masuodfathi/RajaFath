namespace RajaFath.Models
{
    public class HomePageViewModel
    {
        public List<homePostViewModel>? Projects { get; set; }
        public List<homePostViewModel>? News { get; set; }
        public List<homePostViewModel>? Activities { get; set; }
        public List<homePostViewModel>? posts { get; set; }

        public List<Carousel>? Carousel { get; set; }
    }
}
