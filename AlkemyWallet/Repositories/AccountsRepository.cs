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
    }
}
