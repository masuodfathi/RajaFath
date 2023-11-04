using Microsoft.AspNetCore.Identity;

namespace RajaFath.Models
{
    public class PageViewModel
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Content { get; set; }
        public DateTime? UpdateDate { get; set; }
        public IdentityUser? Author {  get; set; }
        //public string? AuthorName 
        //{
        //    set { AuthorName = Author?.UserName; }
        //    get { return AuthorName; }
        //}
        public Image? FeaturImage { get; set; }
        public List<Category>? Categories { get; internal set; }
    }
}
