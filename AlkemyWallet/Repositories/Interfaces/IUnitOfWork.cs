using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<RoleEntity> RolesRepository { get; }
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }
        FixedTermDepositRepository FixedTermDepositRepository { get; }
        AccountsRepository AccountsRepository { get; }
        ICatalogueRepository CatalogueRepository { get; }
        Task Save();
    }
}
