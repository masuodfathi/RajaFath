using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace RajaFath.Models
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }

        
        [MaxLength(10000)]
        [AllowHtml]
        public string? Content { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? Excerpt { get; set; } // A short teaser or summary of the post
        public int? FeaturedImageId { get; set; } // URL or file path to a featured image
        public Image? FeaturedImage { get; set; }
        public string? AuthorId { get; set; } // Reference to the author's user ID
        public IdentityUser? Author { get; set; }// Navigation property for the author
        public string? PostType { get; set; }
        // Additional properties// Additional properties// Additional properties// Additional properties// Additional properties
        // Additional properties// Additional properties// Additional properties// Additional properties// Additional properties
        // Additional properties// Additional properties// Additional properties// Additional properties// Additional properties
        public bool? IsPublished { get; set; } // Whether the post is published or draft
        public string? Slug { get; set; } // A user-friendly URL slug for the post
        public int? Views { get; set; } // Number of views for the post
        public int? Likes { get; set; } // Number of likes for the post
        public int? Dislikes { get; set; } // Number of dislikes for the post
        public string? MetaTitle { get; set; } // Meta title for SEO
        public string? MetaDescription { get; set; } // Meta description for SEO
        public string? CanonicalUrl { get; set; } // Canonical URL for SEO
        
        public string? SourceUrl { get; set; } // URL or source reference for the content
        public bool? IsCommentEnabled { get; set; } // Whether comments are enabled for the post
        public bool? IsArchived { get; set; } // Whether the post is archived
        public int? WordCount { get; set; } // Number of words in the content
        public TimeSpan? ReadingTime { get; set; } // Estimated reading time for the post
        

        

        // Navigation property for comments (assuming you have a Comment model)
        public List<Comment>? Comments { get; set; }

        // Navigation property for related posts
        public List<Post>? RelatedPosts { get; set; } // Posts related to this post
        public List<Post>? RecommendedPosts { get; set; } // Recommended posts

        // Additional properties for SEO and social sharing
        public string? OgTitle { get; set; } // Open Graph title for social sharing
        public string? OgDescription { get; set; } // Open Graph description for social sharing
        public string? TwitterTitle { get; set; } // Twitter card title
        public string? TwitterDescription { get; set; } // Twitter card description

        // Additional properties for scheduling and publishing
        public DateTime? ScheduledPublishDate { get; set; } // Date and time to schedule post publishing
        public bool? IsScheduled { get; set; } // Whether the post is scheduled for future publishing

        // Properties for user interaction
        public int? ShareCount { get; set; } // Number of times the post has been shared
        public int? BookmarkCount { get; set; } // Number of times the post has been bookmarked
        public int? CommentCount { get; set; } // Number of comments on the post
        public int? ReportCount { get; set; } // Number of times the post has been reported

        // Properties for content format
        public bool? IsVideo { get; set; } // Indicates if the post contains video content
        public bool? IsAudio { get; set; } // Indicates if the post contains audio content
        public bool? IsImageGallery { get; set; } // Indicates if the post is an image gallery

        // Properties for content source and attribution
        public string? ContentSource { get; set; } // Source of the post's content
        public string? ContentAttribution { get; set; } // Attribution for the post's content

        // Properties for user engagement and ratings
        public double? AverageRating { get; set; } // Average user rating for the post
        public int? TotalRatings { get; set; } // Total number of user ratings
        public bool? IsUserFavorite { get; set; } // Indicates if the post is marked as a user's favorite

        // Properties for external integration (e.g., social media links)
        public string? FacebookLink { get; set; } // Link to the post on Facebook
        public string? TwitterLink { get; set; } // Link to the post on Twitter
        
        // Navigation property for the associated images
        public List<ImagePost>? ImagePosts { get; set; }

        // Navigation property to represent tags associated with the post
        public List<PostTag>? PostTag { get; set; }

        // Navigation property to represent categories associated with the post
        public List<PostCategory>? PostCategory { get; set; }
    }
}
