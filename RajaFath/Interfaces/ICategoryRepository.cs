using RajaFath.Models;

namespace RajaFath.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryAsync(int CategoryId);
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<bool> AddCategoryAsync(Category category);
        Task<bool> DeleteCategoryAsync(int CategoryId);
        Task<bool> DeleteCategoryRangeAsync(List<int> CategoryIds);
        Task<bool> UpdateCategoryAsync(int CategoryId);
        Task<bool> SaveAsync();
    }
}
