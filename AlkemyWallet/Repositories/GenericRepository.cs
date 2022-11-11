using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
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
            return await _walletDbContext.Set<T>().FirstOrDefaultAsync(e=> e.Id==id);
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
