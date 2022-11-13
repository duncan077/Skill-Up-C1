using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class AccountsRepository: GenericRepository<AccountsEntity>, IAccountRepository
    {
        protected DbSet<AccountsEntity> _dbSet;

        public AccountsRepository(WalletDbContext walletDbContext) : base(walletDbContext)
        {
            _dbSet = walletDbContext.Set<AccountsEntity>();
        }

        public async Task<AccountsEntity> getByUserId(int id)
        {
            return await _dbSet.Include(u => u.User).Where(a => a.UserId == id).FirstAsync();
        }

        public async Task<PagedList<AccountsEntity>> getAll(PagesParameters pagesParams)
        {
            try
            {
                var collection = _dbSet as IQueryable<AccountsEntity>;

                return PagedList<AccountsEntity>.Create(collection,
                pagesParams.PageNumber,
                pagesParams.PageSize);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
