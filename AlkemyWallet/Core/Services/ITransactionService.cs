using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Services
{
    public interface ITransactionService
    {
        Task delete(TransactionEntity entity);
        Task<IReadOnlyList<TransactionEntity>> getAll();
        Task<TransactionEntity> getById(int id);
        Task insert(TransactionEntity entity);
        Task saveChanges();
        Task update(TransactionEntity entity);
    }
}