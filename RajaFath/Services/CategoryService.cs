using RajaFath.Interfaces.Services;
using RajaFath.Models;

namespace RajaFath.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryService _categoryService;
        private readonly IPostService _postService;

        public CategoryService(ICategoryService categoryService, IPostService postService)
        {
            _categoryService = categoryService;
            _postService = postService;
        }
        public async Task<bool> AddCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if (!await _categoryService.CategoryExistAsync(category.CategoryId))
            {
                throw new KeyNotFoundException(nameof(category));
            }
            return await _categoryService.AddCategoryAsync(category);
        }

        public async Task<bool> CategoryExistAsync(int CategoryId)
        {
            if(CategoryId < 0) throw new ArgumentOutOfRangeException(nameof(CategoryId));
            if(!await _categoryService.CategoryExistAsync(CategoryId))
            {
                throw new KeyNotFoundException(nameof(CategoryId));
            }
            return await _categoryService.CategoryExistAsync(CategoryId);
        }

        public async Task<bool> DeleteCategoryAsync(int CategoryId)
        {
            if(CategoryId < 0)
            {
                throw new ArgumentNullException(nameof(CategoryId));
            }
            if(!await _categoryService.CategoryExistAsync(CategoryId))
            {
                throw new KeyNotFoundException(nameof(CategoryId));
            }
            return await _categoryService.DeleteCategoryAsync(CategoryId);
        }

        public async Task<bool> DeleteCategoryRangeAsync(List<int> CategoryIds)
        {
            if (CategoryIds == null) throw new ArgumentNullException(nameof(CategoryIds));
            return await _categoryService.DeleteCategoryRangeAsync(CategoryIds);
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _categoryService.GetCategoriesAsync();
        }

        public async Task<Category> GetCategoryAsync(int CategoryId)
        {
            if(CategoryId < 0) throw new ArgumentOutOfRangeException(nameof(CategoryId));
            if(!await _categoryService.CategoryExistAsync(CategoryId))
            {
                throw new KeyNotFoundException($"Category Id {CategoryId} not exist");
            }
            return await _categoryService.GetCategoryAsync(CategoryId);   
        }

        public async Task<ICollection<Category>> GetPostCategoriesAsync(int postId)
        {
            if(postId < 0) throw new ArgumentOutOfRangeException( nameof(postId));
            if(!await _postService.PostExist(postId))
            {
                throw new KeyNotFoundException($"Post {postId} not exist!");
            }
            return await _categoryService.GetPostCategoriesAsync(postId);
        }

        public async Task<bool> UpdateCategoryAsync(Category category)
        {
            if (category == null) throw new ArgumentNullException(nameof(category));
            if(!await _categoryService.CategoryExistAsync(category.CategoryId))
            {
                throw new KeyNotFoundException($"Category {category} not found!");
            }
            return await _categoryService.UpdateCategoryAsync(category);
        }
    }
}
