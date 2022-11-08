using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Services
{
    public interface IAccountService
    {
        Task<List<AccountDto>> ListedAccounts();
        Task<IReadOnlyList<AccountsEntity>> getAll();
        Task<AccountsEntity> getById(int id);
        Task insert(AccountsEntity entity);
        Task saveChanges();
        Task update(AccountsEntity entity);
        Task TransferAccounts(TransferToAccountsDTO model, int id, string userName);
    }
}