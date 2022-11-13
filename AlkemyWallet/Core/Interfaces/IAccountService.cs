using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IReadOnlyList<AccountsEntity>> getAll();
        Task<PagedList<AccountsEntity>> getAll(int page);
        Task<AccountsEntity> getById(int id);
        Task insert(AccountsEntity entity);
        Task<List<AccountDto>> ListAccounts();
        Task TransferAccounts(TransferToAccountsDTO model, int id, string userName);
        Task update(AccountsEntity account);
        Task delete(AccountsEntity account);
        Task DeleteAccount(AccountsEntity account);
    }
}