namespace RajaFath.Models
{
    public class PostList
    {
        public List<PostInListViewModel>? Posts { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPosts {  get; set; }
        public string? PostType {  get; set; }
        
    }
}
