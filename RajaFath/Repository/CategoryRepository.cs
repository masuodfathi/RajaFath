using Microsoft.EntityFrameworkCore;
using RajaFath.Data;
using RajaFath.Interfaces;
using RajaFath.Models;

namespace RajaFath.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddCategoryAsync(Category category)
        {
            if (category !=  null)
            {
                await _context.Categories.AddAsync(category);
                return  await SaveAsync();
                // or return the category.Id or any other indicator of success
            }
            // Throw a specific exception for better error handling
            throw new ArgumentNullException(nameof(category), "Category cannot be null");
        }

        public async Task<bool> DeleteCategoryAsync(int CategoryId)
        {
            var categoryToDelete = await _context.Categories.FindAsync(CategoryId);
            if (categoryToDelete != null)
            {
                _context.Categories.Remove(categoryToDelete);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<bool> DeleteCategoryRangeAsync(List<int> CategoryIds)
        {
            var categoriesToDelete = await _context.Categories.Where(c => CategoryIds.Contains(c.CategoryId)).ToListAsync();
            if (categoriesToDelete.Count > 0)
            {
                _context.Categories.RemoveRange(categoriesToDelete);
                return await SaveAsync();
            }
            return false;
        }

        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryAsync(int CategoryId)
        {
            if (CategoryId > 0)
            {
                return await _context.Categories.FindAsync(CategoryId);
            }
            throw new KeyNotFoundException($"The category with id {CategoryId} not found");
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

        public async Task<bool> UpdateCategoryAsync(int CategoryId)
        {
            var categoryToUpdate = await _context.Categories.
        }
    }
}
