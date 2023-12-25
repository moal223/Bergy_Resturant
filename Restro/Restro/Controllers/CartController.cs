using Microsoft.AspNetCore.Mvc;
using Restro.Helper;
using Restro.Models;
using Restro.Service;
using Restro.ViewModels;

namespace Restro.Controllers
{
    public class CartController : Controller
    {
        private readonly IDataHelper<Meal> _mealService;
        public CartController(IDataHelper<Meal> mealService)
        {
            _mealService = mealService;
        }

        public async Task<IActionResult> Index()
        {
            var cart = (await SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart")) ?? new List<Item>();
            List<CartMealsAndTotalPrice> items = new List<CartMealsAndTotalPrice>();

            foreach (var item in cart)
            {
                items.Add(new CartMealsAndTotalPrice
                {
                    Item = item,
                    Total = item.Meal.Price * item.Quantity
                });
            }

            ViewData["total"] = items.Sum(item => item.Total);
            return View(items);
        }
        public async Task<IActionResult> AddMeal(int Id) // id 
        {
            if (!User.Identity.IsAuthenticated)
                return RedirectToAction("Login", "Account");

            if (await  SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart") == null)
            {
                var cart = new List<Item>
                {
                    new Item { Meal = await _mealService.GetByIdAsync(Id), Quantity = 1 }
                };
                await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);
            }
            else
            {
                var cart = await  SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart");
                int index = await  isExists(Id);
                if (index != -1)
                    cart[index].Quantity += 1;
                else
                {
                    cart.Add(new Item { Meal = await _mealService.GetByIdAsync(Id), Quantity = 1 });
                }
                await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Remove(int id)
        {
            List<Item> cart = await SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart");
            int index = await isExists(id);
            cart.RemoveAt(0);
            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);

            ViewBag.total = await Task.Run(() => calcTotal(cart));
            return Json(ViewBag.total);
        }
        public async Task<IActionResult> EditTotalPrice(int index, int quantity)
        {
            List<Item> cart = await SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart");
            cart[index].Quantity = quantity;
            await SessionHelper.SetObjectAsJsonAsync(HttpContext.Session, "cart", cart);

            ViewBag.total = await Task.Run(() => calcTotal(cart));
            return Json(ViewBag.total);
        }

        #region Methods
        private async Task<int> isExists(int id)
        {
            var cart = await SessionHelper.GetObjectFromJsonAsync<List<Item>>(HttpContext.Session, "cart");
            for (var i = 0; i < cart.Count; i++)
                if (cart[i].Meal.Id == id)
                    return i;

            return -1;
        }
        private double calcTotal(List<Item> cart)
        {
            double total = 0;
            foreach (var item in cart)
            {
                total += (item.Meal.Price * item.Quantity);
            }
            return total;
        }
        #endregion
    }
}
