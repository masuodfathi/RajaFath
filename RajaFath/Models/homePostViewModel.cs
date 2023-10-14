namespace RajaFath.Models
{
    public class homePostViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Excerpt { get; set; }
        public string? Category { get; set; }
        public string? Author { get; set; }
        public string? ImageUrl { get; set; }
        public string? ImageAlt { get; set;}
        public DateTime? CreatedDate { get; set; }
        public string? TimeDifference { get; set; }

    }
}
