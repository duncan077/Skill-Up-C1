using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IAccountRepository : IGenericRepository<AccountsEntity>
    {
        Task<AccountsEntity> getByUserId(int id);
    }
}
