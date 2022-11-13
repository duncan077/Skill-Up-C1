using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IAccountRepository:IGenericRepository<AccountsEntity>
    {
        Task<AccountsEntity> getByUserId(int id);
        Task<PagedList<AccountsEntity>> getAll(PagesParameters pagesParams);
    }
}
