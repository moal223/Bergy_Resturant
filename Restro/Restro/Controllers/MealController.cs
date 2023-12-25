using Microsoft.AspNetCore.Mvc;
using Restro.Models;
using Restro.Service;
using Restro.ViewModels;

namespace Restro.Controllers
{
    public class MealController : Controller
    {
        private readonly IDataHelper<Meal> _mealService;
        private readonly IDataHelper<PhotoMeal> _photoMealService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public MealController(IDataHelper<Meal> mealService,
            IWebHostEnvironment hostEnvironment,
            IDataHelper<PhotoMeal> photoMealService)
        {
            _mealService = mealService;
            _hostEnvironment = hostEnvironment;
            _photoMealService = photoMealService;
        }
        [HttpGet]
        public IActionResult AddMeal()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddMeal(MealViewModel mealViewModel)
        {
            if (ModelState.IsValid)
            {
                Meal meal = new Meal
                {
                    Name = mealViewModel.Name,
                    Price = mealViewModel.Price,
                    Description = mealViewModel.Description,
                    Rate = mealViewModel.Rate
                };
                // save meal
                var mealModel = await _mealService.AddAsync(meal);
                // save photo meal
                await _photoMealService.AddAsync(await SaveImage(mealViewModel.Photo, mealModel.Id));
                return RedirectToAction("Index", "Home");
            }
            return View(mealViewModel);
        }
  
        public async Task<IActionResult> DeleteMeal(int id)
        {
            if(await _mealService.DeleteAsync(await _mealService.GetByIdAsync(id)) != 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return Json(new { sucess = false });
        }

        #region Methods
        private async Task<PhotoMeal> SaveImage(IFormFile image, int mealId)
        {
            PhotoMeal photoMeal = new PhotoMeal();

            string wwwroot = _hostEnvironment.WebRootPath;

            // identify the img name
            string fileName = Path.GetFileNameWithoutExtension(image.FileName);
            string fileExt = Path.GetExtension(image.FileName);

            // identify the img path
            string path = "\\Img\\" + fileName + fileExt;


            using (var fileStream = new FileStream(wwwroot + "/Img/" + fileName + fileExt, FileMode.Create))
            {
                await image.CopyToAsync(fileStream);
            }
            // give the name an id
            fileName = fileName + Guid.NewGuid().ToString() + fileExt;

            return new PhotoMeal { Name = fileName, Path = path, MealId = mealId };
        }
        #endregion
    }
}
