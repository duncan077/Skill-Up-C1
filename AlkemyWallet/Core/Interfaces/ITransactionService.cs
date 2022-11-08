using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface ITransactionService
    {
        Task delete(TransactionEntity entity);
        Task<IReadOnlyList<TransactionEntity>> getAll();
        Task<TransactionEntity> getById(int id);
        Task insert(TransactionEntity entity);
        Task saveChanges();
        Task update(TransactionEntity entity);
        Task<IReadOnlyList<TransactionEntity>> getTransactionsByUserId(int id);
    }
}