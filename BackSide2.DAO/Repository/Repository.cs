using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackSide2.DAO.Data;
using BackSide2.DAO.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackSide2.DAO.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DataContext _context;

        private readonly DbSet<T> _entities;

        public Repository(DataContext context)
        {
            _context = context;
            _entities = context.Set<T>();
        }

        //public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null)
        //{
        //    if (predicate != null)
        //        return _entities.AsQueryable().Where(predicate);

        //    return _entities.AsQueryable();
        //}

        public async Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null) =>
            await Task.Run(() => predicate != null ? _entities.Where(predicate) : _entities);

        public async Task<T> GetAsync(long id) =>
            await _entities.FirstOrDefaultAsync(e => e.Id == id);

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();
                
            _entities.Add(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            _entities.Update(entity);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            _entities.Remove(entity);
            _context.SaveChanges();
        }

        public async Task RemoveAsync(T entity)
        {
            if (entity == null)
                throw new NullReferenceException();

            _entities.Remove(entity);

            await _context.SaveChangesAsync();
        }

        //public async Task SaveChangesAsync()
        //{
        //    _context.SaveChanges();
        //}
    }
}