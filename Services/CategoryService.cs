using LibrarySPSTApi.Entities;
using LibrarySPSTApi.Interfaces;
using LibrarySPSTApi.data;
using Microsoft.EntityFrameworkCore;

namespace LibrarySPSTApi.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context; // Replace with your DbContext

        public CategoryService(DataContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> GetCategoryByIdAsync(int categoryId)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return category;
        }

        public async Task<Category> UpdateCategoryAsync(int categoryId, Category category)
        {
            var existingCategory = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (existingCategory == null)
            {
                return null; // Or handle as needed
            }

            existingCategory.Name = category.Name;
            existingCategory.Description = category.Description;

            await _context.SaveChangesAsync();
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(int categoryId)
        {
            var categoryToDelete = await _context.Categories.FirstOrDefaultAsync(c => c.Id == categoryId);
            if (categoryToDelete == null)
            {
                return false; // Or handle as needed
            }

            _context.Categories.Remove(categoryToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}