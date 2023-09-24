namespace RajaFath.Models
{
    public class PostCategory
    {
        public string Id { get; set; }
        public string PostsId { get; set; }
        public string CategorysId { get; set; }
        public Post Post { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
