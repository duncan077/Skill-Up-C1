using AlkemyWallet.Entities;
using System.Linq.Expressions;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        //getAll, getById, insert, delete y update

         Task<IReadOnlyList<T>>  getAll();
         Task<T>  getById(int id);
         Task  insert(T entity);
         Task  delete(T entity);
         Task update(T entity);
         Task saveChanges();
        Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null);
        IEnumerable<T> Query(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null);
       
    }
}
