using RajaFath.Models;
using RajaFath.DTOs;
using RajaFath.Interfaces.Repositories;
using RajaFath.Interfaces.Services;

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
                return await _postRepository.AuthorPostCountAsync(authorId);

            // Throw an exception for invalid input
            throw new ArgumentException("AuthorId cannot be null", nameof(authorId));
        }

        public async Task<ICollection<Post>> AuthorPosts(string authorId)
        {
            return await _postRepository.GetAuthorPostsAsync(authorId);
        }

        public async Task<bool> CreatePost(Post post)
        {
            return await _postRepository.CreatePostAsync(post);
        }

        public async Task<bool> DeletePost(int postId)
        {
            return await _postRepository.DeletePostAsync(postId);
        }

        public async Task<Post> GetPost(int postId)
        {
            return await _postRepository.GetPostAsync(postId);
        }

        public async Task<ICollection<Post>> GetPostByType(string postType)
        {
            return await _postRepository.GetPostsByTypeAsync(postType);
        }

        public async Task<ICollection<Post>> GetPosts()
        {
            return await _postRepository.GetPostsAsync();
        }

        public async Task<bool> PostExist(int postId)
        {
            return await _postRepository.PostExistAsync(postId);
        }

        public async Task<bool> PostExist(Post post)
        {
            return await _postRepository.PostExistAsync(post);
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
