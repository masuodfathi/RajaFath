namespace RajaFath.Models
{
    public class Category
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        List<PostCategory>? Posts { get; set; }
    }
}
