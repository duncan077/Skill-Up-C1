using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface ITransactionRepository:IGenericRepository<TransactionEntity>
    {
        Task<IReadOnlyList<TransactionEntity>> getTransactionsByUserId(int id);
    }
}
