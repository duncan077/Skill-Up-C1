using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class TransactionRepository : GenericRepository<TransactionEntity>, ITransactionRepository
    {
        private readonly WalletDbContext _walletDbContext;

        public TransactionRepository(WalletDbContext walletDbContext):base(walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }
        public async Task<IReadOnlyList<TransactionEntity>> getTransactionsByUserId(int id)
        {
            return await _walletDbContext.Set<TransactionEntity>()
                .Include(u=>u.User)
                .ThenInclude(a=>a.Accounts)
                .Where(t => t.UserId == id)
                .OrderByDescending(d=>d.Date)
                .ToListAsync();
        }
        public async Task<PagedList<TransactionEntity>> getAll(PagesParameters pagesParams)
        {
            try
            {
                var collection = _walletDbContext.Transactions as IQueryable<TransactionEntity>;

                collection = collection.Where(a => a.IsDeleted == false);

                return PagedList<TransactionEntity>.Create(collection,
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
