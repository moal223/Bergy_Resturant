using Microsoft.AspNetCore.Mvc;
using Restro.Models;
using Restro.Service;
using System.Diagnostics;

namespace Restro.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDataHelper<Meal> _mealService;

        public HomeController(ILogger<HomeController> logger,
            IDataHelper<Meal> mealService)
        {
            _logger = logger;
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _mealService.GetByAllAsync());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}