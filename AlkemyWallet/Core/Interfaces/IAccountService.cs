using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IAccountService
    {
        Task<IReadOnlyList<AccountsEntity>> getAll();
        Task<AccountsEntity> getById(int id);
        Task insert(AccountsEntity entity);
        Task<List<AccountDto>> ListAccounts();
        Task TransferAccounts(TransferToAccountsDTO model, int id, string userName);
    }
}