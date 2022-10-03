using Microsoft.AspNetCore.Mvc;
using E_Commerce.Data;
using System;
using E_Commerce.Models;

namespace E_Commerce.Data.ViewComponants
{
    public class CartSummary : ViewComponent
        
    {
        private readonly Cart _cart;

        public CartSummary(Cart cart )
        {
            _cart = cart;
        }
        public IViewComponentResult Invoke()
        {
            var item = _cart.GetTotalAmountCount();
            ViewBag.Total
               = _cart.GetCartTotal();
            return View( item );    
        }
    }
}
