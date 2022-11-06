using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AlkemyWallet.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : EntityBase
    {

        private readonly WalletDbContext _walletDbContext;


        public GenericRepository(WalletDbContext walletDbContext)
        {

            _walletDbContext = walletDbContext;


        }
       protected IQueryable<T> QueryDb(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy, Func<IQueryable<T>, IQueryable<T>> includes)
        {
            IQueryable<T> query = _walletDbContext.Set<T>();

            
                query = filter!=null ?  query.Where(filter) : query;
            

           
                query =includes!=null ? includes(query):query;
            

            
                query = orderBy!=null ? orderBy(query):query;
            

            return query;
        }
        public virtual IEnumerable<T> Query(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return result.ToList();
        }

        public virtual async Task<IEnumerable<T>> QueryAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IQueryable<T>> includes = null)
        {
            var result = QueryDb(filter, orderBy, includes);
            return await result.ToListAsync();
        }

        public async Task delete(T entity)
        {
            _walletDbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> getAll()
        {
           
            return await _walletDbContext.Set<T>().ToListAsync();
        }

        public async Task<T> getById(int id)
        {
            return await _walletDbContext.Set<T>().FirstAsync(e=> e.Id==id);
        }

        public async Task insert(T entity)
        {
            await _walletDbContext.AddAsync(entity);
        }

        public async Task saveChanges()
        {
            await _walletDbContext.SaveChangesAsync();
        }

        public async Task update(T entity)
        {
             _walletDbContext.Update(entity);
        }
    }
}
