using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RajaFath.Models;

namespace RajaFath.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
           
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Post>()
                .HasOne(p => p.FeaturedImage)
                .WithMany(fi => fi.Posts)
                .HasForeignKey(p => p.FeaturedImageId);
            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)
                .WithMany()
                .HasForeignKey(p => p.AuthorId);
            modelBuilder.Entity<PostCategory>()
                .HasOne(pc => pc.Post)
                .WithMany(p => p.PostCategory)
                .HasForeignKey(pc => pc.PostId);
            modelBuilder.Entity<PostCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.PostCategory)
                .HasForeignKey(pc => pc.CategoryId);
            modelBuilder.Entity<Carousel>()
                .HasOne(p => p.Post)
                .WithMany()
                .HasForeignKey(c=> c.PostId);
        }
        public DbSet<Image>? Images { get; set; }
        public DbSet<Post>? Posts { get; set; }
        public DbSet<Comment>? Comments { get; set; }
        public DbSet<ImagePost>? ImagePosts { get; set; }
        public DbSet<Tag>? Tags { get; set; }
        public DbSet<Category>? Categories { get; set; }
        public DbSet<PostCategory>? postCategories { get; set; }
        public DbSet<PostTag>? postTags { get; set; }
    }
}