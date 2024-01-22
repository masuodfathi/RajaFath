using RajaFath.Models;

namespace RajaFath.Interfaces
{
    public interface IPostRepository
    {
        Task<Post> GetPost(int postId);
        Task<ICollection<Post>> GetPostByType(string postType);
        Task<ICollection<Post>> GetPosts();
        Task<bool> PostExict(int postId);
        Task<bool> PostExict(Post post);
        Task<bool> CreatePost(Post post);
        Task<bool> UpdatePost(Post post,int postId);
        Task<bool> DeletePost(int postId);
        Task<int> AuthorPostCount(string authorId);
        Task<bool> UpdatePostCategories(int  postId, ICollection<int> categoryIds);
        Task<bool> Save();
    }
}
