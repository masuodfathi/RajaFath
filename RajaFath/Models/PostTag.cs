using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace RajaFath.Models
{
    public class PostTag
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string PostId { get; set; }
        public string TagId { get; set; }
        public Post Post { get; set; } = null!;
        public Tag Tag { get; set; } = null!;
    }
}
