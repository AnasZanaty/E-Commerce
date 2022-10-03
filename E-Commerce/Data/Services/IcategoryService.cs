using E_Commerce.Data.Base;
using E_Commerce.Models;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_Commerce.Data.Services
{
    public interface IcategoryService : IEntityBaseRepositary<category>
    {
        //comment all methods cause it have been implemented in the interface


        ////5 functions are nesssecary 

        //Task<IEnumerable<category>> GetAllAsync();
        //Task <category>GetByIdAsync(int id);
        //Task CreateAsync(category entity);

        //Task UpdateAsync (category entity);
        //Task DeleteAsync(int id);

    }
}
