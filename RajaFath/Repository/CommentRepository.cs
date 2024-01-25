using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces;
using RajaFath.Models;
using System.Web.Mvc;

namespace RajaFath.Repository
{
    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _context;

        public CommentRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCommentAsync(Comment comment)
        {
            if (comment == null) throw new ArgumentNullException(nameof(comment));
            _context.Comments.AddAsync(comment);
            return await SaveAsync();
        }

        public async Task<bool> DeleteCommentAsync(int commentId)
        {
            var removingComment = await _context.Comments.FindAsync(commentId);
            if (removingComment != null)
            {
                _context.Comments.Remove(removingComment);
                return await SaveAsync();
            }
            throw new KeyNotFoundException($"The comment with id {commentId} is not found");
        }

        public async Task<Comment> GetCommentAsync(int commentId)
        {
            if (commentId > 0)
            {
                return await _context.Comments.FindAsync(commentId);
            }
            throw new ArgumentOutOfRangeException(nameof(commentId));
        }

        public async Task<ICollection<Comment>> GetCommentsAsync()
        {
            return await _context.Comments.ToListAsync();
        }

        public async Task<ICollection<Comment>> GetCommentsOfPostAsync(int postId)
        {
            if(postId > 0)
            {
                return await _context.Comments.Where(c => c.PostId == postId).ToListAsync();
            }
            throw new KeyNotFoundException($"the post with id {postId} is not found");
        }

        public async Task<ICollection<Comment>> GetCommentsOfUserAsync(string userId)
        {
            if (!string.IsNullOrEmpty(userId))
            {
                return await _context.Comments.Where(c => c.UserId == userId).ToListAsync();
            }
            throw new KeyNotFoundException($"the user with id {userId} is not found");
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
    }
}
