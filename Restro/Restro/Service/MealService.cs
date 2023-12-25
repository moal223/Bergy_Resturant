using Microsoft.EntityFrameworkCore;
using Restro.Models;

namespace Restro.Service
{
    public class MealService : IDataHelper<Meal>
    {
        private readonly AppDbContext _context;
        private readonly IDataHelper<PhotoMeal> _photoMeal;
        public MealService(AppDbContext context, IDataHelper<PhotoMeal> photoMeal)
        {
            _context = context;
            _photoMeal = photoMeal;
        }
        public async Task<Meal> AddAsync(Meal entity)
        {
            var meal = (await _context.Meals.AddAsync(entity)).Entity;
            await _context.SaveChangesAsync();
            return meal;
        }

        public async Task<int> DeleteAsync(Meal entity)
        {
            await _photoMeal.DeleteAsync(entity.PhotoMeal);
            if (await Task.Run(() => _context.Meals.Remove(entity)) is not null)
            {
                await _context.SaveChangesAsync();
            }
                return 1;
            return 0;
        }

        public async Task<IList<Meal>> GetByAllAsync()
        {
            return (await Task.Run(() => _context.Meals.Include(m => m.PhotoMeal))).ToList();
        }

        public async Task<Meal> GetByIdAsync(int id)
        {
            return await _context.Meals.Include(m => m.PhotoMeal).FirstOrDefaultAsync(m => m.Id == id);
        }

        public Task<int> UpdateAsync(Meal entity)
        {
            throw new NotImplementedException();
        }
    }
}
