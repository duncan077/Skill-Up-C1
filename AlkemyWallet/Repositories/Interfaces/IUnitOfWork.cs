using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<RoleEntity> RolesRepository { get; }
        IGenericRepository<TransactionEntity> TransactionRepository { get; }
        IGenericRepository<UserEntity> UserRepository { get; }
        IGenericRepository<FixedTermDepositEntity> FixedTermDepositRepository { get; }
        IGenericRepository<AccountsEntity> AccountsRepository { get; }
        IGenericRepository<CatalogueEntity> CatalogueRepository { get; }

        void Save();
    }
}
