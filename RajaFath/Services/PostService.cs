using RajaFath.Interfaces;
using RajaFath.Models;
using RajaFath.DTOs;

namespace RajaFath.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }
        public async Task<int> AuthorPostCount(string authorId)
        {
            if(authorId != null)
                return await _postRepository.AuthorPostCount(authorId);

            // Throw an exception for invalid input
            throw new ArgumentException("AuthorId cannot be null", nameof(authorId));
        }
        public async Task<bool> CreatePost(Post post)
        {
            return await _postRepository.CreatePost(post);
        }

        public async Task<bool> DeletePost(int postId)
        {
            return await _postRepository.DeletePost(postId);
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postRepository.GetPost(postId);
        }

        public async Task<ICollection<Post>> GetPostByType(string postType)
        {
            return await _postRepository.GetPostsByType(postType);
        }

        public async Task<ICollection<Post>> GetPosts()
        {
            return await _postRepository.GetPosts();
        }

        public async Task<bool> PostExist(int postId)
        {
            return await _postRepository.PostExist(postId);
        }

        public async Task<bool> PostExist(Post post)
        {
            return await _postRepository.PostExist(post);
        }

        public async Task<bool> UpdatePost(PostUpdateDTO post, int postId)
        {
            //var oldPost = await _postRepository.GetPost(postId);
            //if (oldPost != null)
            //{
            //    return await _postRepository.UpdatePost(post, postId);
            //}
            //throw new KeyNotFoundException($"Post with Id {postId} not found.");
            throw new NotImplementedException();
            
        }

        public Task<bool> UpdatePostCategories(int postId, ICollection<int> categoryIds)
        {
            throw new NotImplementedException();
        }
    }
}
