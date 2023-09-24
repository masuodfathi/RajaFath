using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RajaFath.Models
{
    public class Tag
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Usage {  get; set; }

        List<PostTag> PostsTag { get; set; }
    }
}
