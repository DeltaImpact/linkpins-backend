using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackSide2.DAO.Entities;

namespace BackSide2.DAO.Repository
{
    public interface IRepository<T> where T : BaseEntity
    {
        Task<IQueryable<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null);
        Task<T> GetAsync(long id);
        Task InsertAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
        Task RemoveAsync(T entity);
        //Task SaveChangesAsync();

    }
}
