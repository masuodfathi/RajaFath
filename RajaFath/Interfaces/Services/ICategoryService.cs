using RajaFath.Models;

namespace RajaFath.Interfaces.Services
{
    public interface ICategoryService
    {
        Task<Category> GetCategoryAsync(int CategoryId);
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<ICollection<Category>> GetPostCategoriesAsync(int postId);
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int CategoryId);
        Task<bool> DeleteCategoryRangeAsync(List<int> CategoryIds);
        Task<bool> UpdateCategoryAsync(Category category);
        Task<bool> CategoryExistAsync(int CategoryId);
    }
}
