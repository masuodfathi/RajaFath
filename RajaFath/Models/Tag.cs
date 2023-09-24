namespace RajaFath.Models
{
    public class Tag
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Usage {  get; set; }

        List<PostTag> Posts { get; set; }
    }
}
