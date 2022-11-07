using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<RoleEntity> RolesRepository { get; }
        IGenericRepository<TransactionEntity> TransactionRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<FixedTermDepositEntity> FixedTermDepositRepository { get; }
        AccountsRepository AccountsRepository { get; }
        
        Task Save();
    }
}
