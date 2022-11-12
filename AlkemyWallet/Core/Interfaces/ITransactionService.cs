using AlkemyWallet.Core.Helper;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        Task delete(TransactionEntity entity);
     
        Task<TransactionEntity> getById(int id);
        Task insert(TransactionEntity entity);
        Task saveChanges();
        Task update(TransactionEntity entity);
        Task<PagedList<TransactionEntity>> getTransactionsByUserId(int id);
        Task UpdateTransaction(TransactionEntity entity, int id);

        Task DeleteTransaction(int id);
        Task<IReadOnlyList<TransactionEntity>> getAll(int page);
    }
}