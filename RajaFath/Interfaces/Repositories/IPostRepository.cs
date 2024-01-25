using RajaFath.Models;

namespace RajaFath.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetPost(int postId);
        Task<ICollection<Post>> GetPostsByType(string postType);
        Task<ICollection<Post>> GetPostsByCategory(int categoryId);
        Task<ICollection<Post>> GetPosts();
        Task<bool> PostExist(int postId);
        Task<bool> PostExist(Post post);
        Task<bool> CreatePost(Post post);
        Task<bool> UpdatePost(Post post, int postId);
        Task<bool> DeletePost(int postId);
        Task<int> AuthorPostCount(string authorId);
        Task<bool> UpdatePostCategories(int postId, ICollection<int> categoryIds);
        Task<bool> Save();
    }
}
