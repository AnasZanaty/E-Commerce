using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace E_Commerce.Data.Base
{
    public class EntityBaesRepository<T> : IEntityBaseRepositary<T> where T : class, IBaseEntity
    {
        private readonly E_CommerceDbContext _context;
        private readonly DbSet<T> _entities;
        public EntityBaesRepository(E_CommerceDbContext context)
        {
            _context = context;
            _entities = _context.Set<T>();

        }
        public async Task CreateAsync(T entity)
        {
            await _entities.AddAsync(entity);
            await SaveChanges(); //instead of writing await _context.savechanges

        }

        public async Task DeleteAsync(int id)
        {
            var entityId = await _entities.FirstOrDefaultAsync(x => x.Id == id);
                if (entityId != null)
            {
                _entities.Remove(entityId);
                await SaveChanges();
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var RESULT = await _entities.ToListAsync();
            return RESULT;
        }
        //or => await _entities.ToListAsync 

        public async Task<T> GetByIdAsync(int id)
        {
            var result = await _entities.FirstOrDefaultAsync(x => x.Id == id);
            return result;  

        }
        //or => await _entities.FirstOrDefaultAsync

        public async Task UpdateAsync(T entity)
        {
            EntityEntry entityEntry = _context.Entry<T>(entity);
            entityEntry.State = EntityState.Modified;
            await SaveChanges();

        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] include)
        {
            IQueryable <T> query = _entities.AsQueryable();
            query = include.Aggregate(query , (Current,include) => Current.Include(include));


            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] include)
        {
            IQueryable<T> query = _entities.AsQueryable();
            query = include.Aggregate(query, (Current, include) => Current.Include(include));
            return await query.FirstOrDefaultAsync(x=>x.Id==id);

        }
    }
}
