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
                .ThenInclude(a=>a.Account)
                .Where(t => t.UserId == id)
                .OrderByDescending(d=>d.Date)
                .ToListAsync();
        }
    }
}
