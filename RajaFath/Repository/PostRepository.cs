using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces;
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
        public async Task<int> AuthorPostCount(string authorId)
        {
            return _context.Posts.Count(p => p.Author.Id == authorId);
        }

        public async Task<bool> CreatePost(Post post)
        {
            if(post != null)
            {
                await _context.Posts.AddAsync(post);
                return await Save();
            }
            return false;
            
        }

        public async Task<bool> DeletePost(int postId)
        {
            if(postId  > 0)
            {
                var postToRemove =await GetPost(postId);
                _context.Posts.Remove(postToRemove);
                return await Save();
            }
            return false;
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _context.Posts.FindAsync(postId);
        }

        public async Task<ICollection<Post>> GetPostByType(string postType)
        {
            return await _context.Posts.Where(p => p.PostType == postType).ToListAsync();
        }

        public async Task<ICollection<Post>> GetPosts()
        {
            return await _context.Posts.ToListAsync();
        }

        public async Task<bool> PostExict(int postId)
        {
            return await _context.Posts.AnyAsync(p => p.Id == postId);
        }

        public async Task<bool> PostExict(Post post)
        {
            return await PostExict(post.Id);
        }

        public async Task<bool> Save()
        {
            try
            {
                var save = await _context.SaveChangesAsync();
                if (save > 0)
                {
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> UpdatePost(Post post, int postId)
        {
            _context.Posts.Update(post);
            return await Save();
        }

        public async Task<bool> UpdatePostCategories(int postId, ICollection<int> categoryIds)
        {
            //Remove exict category
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

            return await Save();

        }
    }
}
