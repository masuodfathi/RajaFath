using RajaFath.Models;

namespace RajaFath.Interfaces.Repositories
{
    public interface IPostRepository
    {
        Task<Post> GetPostAsync(int postId);
        Task<ICollection<Post>> GetPostsByTypeAsync(string postType);
        Task<ICollection<Post>> GetPostsByCategoryAsync(int categoryId);
        Task<ICollection<Post>> GetPostsAsync();
        Task<bool> PostExistAsync(int postId);
        Task<bool> PostExistAsync(Post post);
        Task<bool> CreatePostAsync(Post post);
        Task<bool> UpdatePostAsync(Post post, int postId);
        Task<bool> DeletePostAsync(int postId);
        Task<ICollection<Post>> GetAuthorPostsAsync(string authorId);
        Task<int> AuthorPostCountAsync(string authorId);
        Task<bool> UpdatePostCategoriesAsync(int postId, ICollection<int> categoryIds);
        Task<bool> SaveAsync();
    }
}
