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
    
        public async Task<PagedList<TransactionEntity>> getAll(PagesParameters pagesParams, int userId)
        {
            try
            {
                var collection = _walletDbContext.Transactions as IQueryable<TransactionEntity>;

                collection = collection.Where(a => a.IsDeleted == false && a.UserId==userId).OrderByDescending(d=>d.Date);

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
