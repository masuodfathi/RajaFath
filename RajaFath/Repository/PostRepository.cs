using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces.Repositories;
using RajaFath.Models;

namespace RajaFath.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDbContext _context;

        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<int> AuthorPostCountAsync(string authorId)
        {
            return _context.Posts.Count(p => p.Author.Id == authorId);
        }

        public async Task<bool> CreatePostAsync(Post post)
        {
            if(post != null)
            {
                await _context.Posts.AddAsync(post);
                return await SaveAsync();
            }
            return false;
            
        }

        public async Task<bool> DeletePostAsync(int postId)
        {
            if(postId  > 0)
            {
                var postToRemove =await GetPostAsync(postId);
                _context.Posts.Remove(postToRemove);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<Post> GetPostAsync(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<ICollection<Post>> GetPostsByTypeAsync(string postType)
        {
            return await _context.Posts.Where(p => p.PostType == postType).ToListAsync();
        }

        public async Task<ICollection<Post>> GetPostsAsync()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> PostExistAsync(int postId)
        {
            return await _context.Posts.AnyAsync(p => p.Id == postId);
        }

        public async Task<bool> PostExistAsync(Post post)
        {
            return await PostExistAsync(post.Id);
        }

        public async Task<bool> SaveAsync()
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return true;
                }
                throw new DbUpdateException("Error updating the database.");

            }
            catch (Exception ex)
            {
                throw new DbUpdateException("Error updating the database.", ex);
            }
        }

        public async Task<bool> UpdatePostAsync(Post post, int postId)
        {
            var oldPost = await _context.Posts.FindAsync(postId);
            if (oldPost == null)
            {
                throw new KeyNotFoundException($"Post with Id {postId} not found.");
            }
            _context.Posts.Update(post);
            return await SaveAsync();
        }

        public async Task<bool> UpdatePostCategoriesAsync(int postId, ICollection<int> categoryIds)
        {
            //Remove exist category
            var existingCategories = await _context.postCategories
                .Where(pc => pc.PostId == postId)
                .ToListAsync();
            if (existingCategories != null && existingCategories.Count > 0)
                _context.postCategories.RemoveRange(existingCategories);

            //Add new categories
            foreach (var category in categoryIds)
            {
                _context.postCategories.Add(new PostCategory { PostId = postId, CategoryId = category });
            }

            return await SaveAsync();

        }

        public async Task<ICollection<Post>> GetPostsByCategoryAsync(int categoryId)
        {
            return await _context.Posts.Where(p => p.PostCategory.Any(pc => pc.CategoryId == categoryId)).ToListAsync();
            
        }

        public async Task<ICollection<Post>> GetAuthorPostsAsync(string authorId)
        {
            return await _context.Posts.Where(p => p.AuthorId == authorId).ToListAsync();
        }
    }
}
