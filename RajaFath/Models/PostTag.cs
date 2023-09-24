namespace RajaFath.Models
{
    public class PostTag
    {
        public string Id { get; set; }
        public string PostsId { get; set; }
        public string TagsId { get; set; }
        public Post Post { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
