using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class FixedTermDepositRepository : GenericRepository<FixedTermDepositEntity>, IFixedTermDepositRepository
    {
        private readonly WalletDbContext _walletDbContext;

        public FixedTermDepositRepository(WalletDbContext walletDbContext) : base(walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public async Task<IReadOnlyList<FixedTermDepositEntity>> getFixedTermDepositByUserId(int id)
        {
            return await _walletDbContext.Set<FixedTermDepositEntity>()
                .Include(u => u.User)
                .ThenInclude(a => a.Account)
                .Where(t => t.UserId == id)
                .OrderByDescending(d => d.CreationDate)
                .ToListAsync();
        }
    }
}
