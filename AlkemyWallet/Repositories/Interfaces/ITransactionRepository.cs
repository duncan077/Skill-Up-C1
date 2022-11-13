using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface ITransactionRepository:IGenericRepository<TransactionEntity>
    {
        Task<PagedList<TransactionEntity>> getAll(PagesParameters pagesParams, int userId);
        
    }
}
