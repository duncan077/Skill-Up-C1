using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Entities;
using System.Linq.Expressions;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IUserService
    {

        Task delete(UserEntity entity);
        Task<IReadOnlyList<UserDTO>> getAll();
        Task<UserEntity> getById(int id);
        Task<UserEntity> getByUserName(string username);
        Task insert(UserEntity entity);
   
        Task saveChanges();
        Task update(UserEntity entity);
        Task<CatalogueDTO> GetCatalogueById(int idProduct);
        Task<PagedList<UserEntity>> getAll(int page);
        Task<AccountsEntity> GetAccountByID(int id);
        Task blockAccount(AccountsEntity account);
        Task unblockAccount(AccountsEntity account);


    }
}
