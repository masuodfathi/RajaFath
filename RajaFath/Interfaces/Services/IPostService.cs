using RajaFath.DTOs;
using RajaFath.Models;

namespace RajaFath.Interfaces.Services
{
    public interface IPostService
    {
        Task<Post> GetPost(int postId);
        Task<ICollection<Post>> GetPostByType(string postType);
        Task<ICollection<Post>> GetPosts();
        Task<bool> PostExist(int postId);
        Task<bool> PostExist(Post post);
        Task<bool> CreatePost(Post post);
        Task<bool> UpdatePost(PostUpdateDTO post, int postId);
        Task<bool> DeletePost(int postId);
        Task<int> AuthorPostCount(string authorId);
        Task<bool> UpdatePostCategories(int postId, ICollection<int> categoryIds);
    }
}
