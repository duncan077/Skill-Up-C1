using AlkemyWallet.Entities;
using System.Linq.Expressions;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IUserService
    {

        Task delete(UserEntity entity);
        Task<IReadOnlyList<UserEntity>> getAll();
        Task<UserEntity> getById(int id);
        Task<UserEntity> getByUserName(string username);
        Task insert(UserEntity entity);
   
        Task saveChanges();
        Task update(UserEntity entity);

    }
}
