using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RajaFath.Models
{
    public class PostCategory
    {
        [Key]
        public string Id { get; set; }
        public string PostId { get; set; }
        public string CategoryId { get; set; }
        public Post Post { get; set; } = null!;
        public Category Category { get; set; } = null!;
    }
}
