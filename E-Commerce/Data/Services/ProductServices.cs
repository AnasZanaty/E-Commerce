using E_Commerce.Data.Base;
using E_Commerce.Models;

namespace E_Commerce.Data.Services
{
    public class ProductServices : EntityBaesRepository<product>, IProductServices
    {
        public ProductServices(E_CommerceDbContext context): base (context)
        {

        }
    }
}
