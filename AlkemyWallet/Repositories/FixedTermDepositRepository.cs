using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
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
                .ThenInclude(a => a.Accounts)
                .Where(t => t.UserId == id)
                .OrderByDescending(d => d.CreationDate)
                .ToListAsync();
        }


        public async Task<PagedList<FixedTermDepositEntity>> getAll(PagesParameters pagesParams)
        {
            try
            {
                var collection = _walletDbContext.FixedTermDeposits as IQueryable<FixedTermDepositEntity>;

                collection = collection.Where(a => a.IsDeleted == false);

                return PagedList<FixedTermDepositEntity>.Create(collection,
                pagesParams.PageNumber,
                pagesParams.PageSize);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<PagedList<FixedTermDepositEntity>> getAll(PagesParameters pagesParams, int userId)
        {
            try
            {
                var collection = _walletDbContext.FixedTermDeposits.Where(a => a.UserId == userId&&a.IsDeleted==false);
           

                return PagedList<FixedTermDepositEntity>.Create(collection,
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
