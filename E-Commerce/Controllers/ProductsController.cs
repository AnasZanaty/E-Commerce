using E_Commerce.Data;
using E_Commerce.Data.Services;
using E_Commerce.Data.Static;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{

    public class ProductsController : Controller
    {
        private readonly IcategoryService _CategoryServices;

        private readonly IProductServices _services;
        public ProductsController(IProductServices services , IcategoryService CategoryServices)
            
        {
            _services = services;
            _CategoryServices = CategoryServices;
        }

        public async Task <IActionResult> Index( string SearchTerm)
        {
//SearchTerm is the Name of Search input in the Layout
            var response = await _services.GetAllAsync(x => x.category);

            if (!string.IsNullOrEmpty(SearchTerm))
            {
                response = response.Where(x=>x.Name.Contains(SearchTerm)).ToList();
            }
            return View(response);
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _services.GetByIdAsync(id,x=>x.category);
            return View(product);


        }
        [HttpGet]
        public async Task <IActionResult> create()
        {
            ViewBag.categoryId = await _CategoryServices.GetAllAsync();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> create(product product)
        {
            if (ModelState.IsValid)
            {
                await _services.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            return View("NotFound");
        }
        [HttpGet]
        public async Task <IActionResult> Edit (int id)
        {
           ViewBag.category = await _CategoryServices.GetAllAsync();

            var ProductId = await _services.GetByIdAsync(id, x => x.category);
            return View(ProductId);

        }
        [HttpPost]
        public async Task<IActionResult> Edit(product product)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound");

            }


            await _services.UpdateAsync(product);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> delete(int id)
        {
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));


        }

    }
}
