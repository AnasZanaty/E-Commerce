using E_Commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Commerce.Data.Base
{
    public interface IEntityBaseRepositary<T>where T : class, IBaseEntity 
    {
        //from category sevices
        Task<IEnumerable<T>> GetAllAsync();


        //OVERLOADING INCLUDE METHOD
        Task<IEnumerable<T>> GetAllAsync(params Expression <Func<T, object>>[] include);

        Task<T> GetByIdAsync(int id);
        Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include);

        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task SaveChanges();
    }
}
