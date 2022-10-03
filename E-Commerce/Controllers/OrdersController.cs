using E_Commerce.Data.Services;
using E_Commerce.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    public class OrdersController : Controller
    {
        private readonly Cart _cart;
        private readonly IProductServices _services;
        public OrdersController(IProductServices services, Cart cart)
        {
            _services = services;
            _cart = cart;
        }

        public IActionResult Index()
        {
            var item = _cart.GetCartItems();
            ViewBag.Total =_cart.GetCartTotal();
            return View(item);
        }
  
        public async Task<IActionResult> AddToCart(int id)
        {
            var item = await _services.GetByIdAsync(id);
            if (item != null)
            {
                await _cart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> RemoveFromCart(int id)
        {
            var item = await _services.GetByIdAsync(id);
            if (item != null)
            {
                await _cart.RemoveItem(item);
            }
            return RedirectToAction(nameof(Index));

        }
        }
    }
