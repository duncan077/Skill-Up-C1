using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IRolesRepository RolesRepository { get; }
        IGenericRepository<TransactionEntity> TransactionRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<FixedTermDepositEntity> FixedTermDepositRepository { get; }
        IGenericRepository<AccountsEntity> AccountsRepository { get; }
        
        void Save();
    }
}
