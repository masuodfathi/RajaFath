using RajaFath.Interfaces.Repositories;
using RajaFath.Interfaces.Services;
using RajaFath.Models;

namespace RajaFath.Services
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentService(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            return await _commentRepository.AddCommentAsync(comment);
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            return await _commentRepository.DeleteCommentAsync(commentId);
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            return await _commentRepository.GetCommentAsync(commentId);
        }

        public async Task<ICollection<Comment>> GetCommentsAsync()
        {
            return 
        }

        public Task<ICollection<Comment>> GetCommentsOfPostAsync(int postId)
        {
            throw new NotImplementedException();
        }

        public Task<ICollection<Comment>> GetCommentsOfUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveAsync()
        {
            throw new NotImplementedException();
        }
    }
}
