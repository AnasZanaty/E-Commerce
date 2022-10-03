using E_Commerce.Data.Base;
using E_Commerce.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Data.Services
{
    public class CategoryServices : EntityBaesRepository<category>, IcategoryService
    {
        public CategoryServices(E_CommerceDbContext context) : base(context)
        {
        }
    }
      
}
