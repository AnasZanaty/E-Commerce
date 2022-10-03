using E_Commerce.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Data
{

    public class E_CommerceDbContext :IdentityDbContext<ApplicationUser>
    {
        public E_CommerceDbContext(DbContextOptions<E_CommerceDbContext>options): base(options)
        {
                
        }
        public DbSet<product> products { get; set; }
        public DbSet<category> categories { get; set; }
        public DbSet<order> orders { get; set; }
        public DbSet<OrdersDetails> OrdersDetails { get; set; }

        public DbSet<CartItem> CartItems { get; set; }


    }
}
