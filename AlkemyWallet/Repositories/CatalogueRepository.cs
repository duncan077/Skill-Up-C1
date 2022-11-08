
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class CatalogueRepository : GenericRepository<CatalogueEntity>, ICatalogueRepository
    {
        private readonly WalletDbContext _walletDbContext;

        public CatalogueRepository(WalletDbContext walletDbContext) : base(walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public async Task<IReadOnlyList<CatalogueEntity>> getCatalogueOrderByPoints()
        {
            return await _walletDbContext.Set<CatalogueEntity>().OrderBy(c => c.Points).ToListAsync();
        }
    }
}

