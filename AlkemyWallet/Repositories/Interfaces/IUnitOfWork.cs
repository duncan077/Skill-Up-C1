using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        IUserRepository UserRepository { get; }
        IFixedTermDepositRepository FixedTermDepositRepository { get; }
        AccountsRepository AccountsRepository { get; }
        ICatalogueRepository CatalogueRepository { get; }
        Task Save();
        IRolesRepository RolesRepository { get; }
    }
}
