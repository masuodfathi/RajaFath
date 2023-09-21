using System.ComponentModel.DataAnnotations;

namespace RajaFath.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string? Content { get; set; } // The content of the comment

        public DateTime? CreatedAt { get; set; } // Date and time when the comment was created

        // Foreign key for relating the comment to a specific post
        public int? PostId { get; set; }
        public Post? Post { get; set; }

        // User-related properties
        public string? UserId { get; set; } // ID of the user who posted the comment
        //public ApplicationUser? User { get; set; } // User who posted the comment

        // Additional properties
        public bool? IsApproved { get; set; } // Indicates if the comment is approved or pending moderation
        public int? Upvotes { get; set; } // Number of upvotes received by the comment
        public int? Downvotes { get; set; } // Number of downvotes received by the comment

        // You can include more properties as needed
    }
}
