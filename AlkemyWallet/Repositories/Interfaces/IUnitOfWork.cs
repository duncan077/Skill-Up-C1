using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<RoleEntity> RolesRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }
        IGenericRepository<FixedTermDepositEntity> FixedTermDepositRepository { get; }
        AccountsRepository AccountsRepository { get; }
        IGenericRepository<CatalogueEntity> CatalogueRepository { get; }
        Task Save();
    }
}
