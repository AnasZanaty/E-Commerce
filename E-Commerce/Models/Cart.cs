using E_Commerce.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Commerce.Models
{
    public class Cart
    {
        private readonly E_CommerceDbContext _context;

        public string CartId { get; set; }
        public Cart(E_CommerceDbContext context)
        {
           _context = context;
                
        }

        //Get ALL item in shopping cart
        #region session
        public static Cart GetShoppingCart(IServiceProvider service)
        {
            var session = service.GetRequiredService<IHttpContextAccessor>().HttpContext.Session;
            var context = service.GetService<E_CommerceDbContext>();
            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);
            return new Cart(context) { CartId = cartId };
        }
        #endregion 
        public List<CartItem> GetCartItems()
        {
         return _context.CartItems.Where(x => x.CartId == CartId).Include(x=>x.Product).ToList();

        }
        // Calculate total amount in shopping cart item
        public double GetCartTotal()

        {
            var total = _context.CartItems.Where(x => x.CartId == CartId).Select(x => x.Product.price * x.Amount).Sum();
            return total;

        }
        public int GetTotalAmountCount()

        {
            var total = _context.CartItems.Where(x => x.CartId == CartId).Select(x =>x.Amount).Sum();
            return total;

        }
        public async Task AddItemToCart(product product)
        {
            //if product found so i have to +1 the product or its not found so i have to add it for first time
            var cartitem = await _context.CartItems.FirstOrDefaultAsync(x => x.CartId == CartId && x.Product.Id == product.Id);
                if (cartitem == null) //not found
            { //add it using anymonous object
                cartitem = new CartItem()
                {
                    CartId = CartId,
                    Product = product,
                    Amount = 1

                };
                await _context.CartItems.AddAsync(cartitem);
                await _context.SaveChangesAsync();

            }
                //found just do +1
                else
            {
                cartitem.Amount += 1;
                await _context.SaveChangesAsync();

            }

        }

        public async Task RemoveItem(product product)
        {
            var cartitem = await _context.CartItems.FirstOrDefaultAsync(x => x.CartId == CartId && x.Product.Id == product.Id);
            if (cartitem != null)
            {
                if(cartitem.Amount > 1)
                {
                    cartitem.Amount = cartitem.Amount - 1;
                     await _context.SaveChangesAsync();
                }
                else
                {
                    _context.CartItems.Remove(cartitem);
                    await _context.SaveChangesAsync();
                }


            }

        }

        internal object CartItems()
        {
            throw new NotImplementedException();
        }
    }
}
