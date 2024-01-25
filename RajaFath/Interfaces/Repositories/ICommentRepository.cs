using RajaFath.Models;

namespace RajaFath.Interfaces.Repositories
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentAsync(int commentId);
        Task<ICollection<Comment>> GetCommentsAsync();
        Task<ICollection<Comment>> GetCommentsOfPostAsync(int postId);
        Task<ICollection<Comment>> GetCommentsOfUserAsync(string userId);
        Task<bool> AddCommentAsync(Comment comment);
        Task<bool> DeleteCommentAsync(int commentId);
        Task<bool> SaveAsync();

    }
}
