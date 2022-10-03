using E_Commerce.Data;
using E_Commerce.Data.Services;
using E_Commerce.Data.Static;
using E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Controllers
{
    public class CategoriesController : Controller
    {
        //Dependancy Injection 3 steps

        //1) dependancy property _context

        private readonly IcategoryService _services ;

        //2) constructor with context parameter
        public CategoriesController(IcategoryService services)
        {
        //3) parameter = property
            _services = services;
        }

        //using async to prevent happening of pipline 
        public async Task <IActionResult> Index()
        {
            //variable load the context data of category

            var response = await _services.GetAllAsync();
            return View(response);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        
        //binding for selecting a specific attributes
        public async Task<IActionResult>create ([Bind ("Name , Description")]category category)
        {
            if (ModelState.IsValid)
            {
                await _services.CreateAsync (category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }
        [HttpGet]
        public async Task<IActionResult>Details(int id)
        {
            var category = await _services.GetByIdAsync(id);
            if(category!= null)
            {
                return View(category);
            }
            return View("NotFound");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _services.GetByIdAsync(id);
            if (category != null)
            {
                return View(category);
            }
            return View("NotFound");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(category category)
        {
            if (!ModelState.IsValid)
            {
                return View("NotFound");

            }


            await _services.UpdateAsync(category);
            return RedirectToAction(nameof(Index));
        }
        public async Task <IActionResult>delete (int id)
        {
            await _services.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }



    }
}
