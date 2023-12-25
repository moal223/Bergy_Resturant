using Microsoft.EntityFrameworkCore;
using Restro.Models;

namespace Restro.Service
{
    public class PhotoMealService : IDataHelper<PhotoMeal>
    {
        private readonly AppDbContext _context;
        public PhotoMealService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<PhotoMeal> AddAsync(PhotoMeal entity)
        {
            var photo = (await _context.PhotoMeals.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return photo;
        }

        public async Task<int> DeleteAsync(PhotoMeal entity)
        {
            if (await Task.Run(() => _context.PhotoMeals.Remove(entity)) is not null)
            {
                await _context.SaveChangesAsync();
                return 1;
            }

            return 0;
        }

        public async Task<IList<PhotoMeal>> GetByAllAsync()
        {
            return (await Task.Run(() => _context.PhotoMeals)).ToList();
        }

        public async Task<PhotoMeal> GetByIdAsync(int id)
        {
            return await _context.PhotoMeals.FirstOrDefaultAsync(p => p.MealId == id);
        }

        public Task<int> UpdateAsync(PhotoMeal entity)
        {
            throw new NotImplementedException();
        }
    }
}
