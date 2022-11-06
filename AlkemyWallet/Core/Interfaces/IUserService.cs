using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IUserService
    {

        Task delete(UserEntity entity);
        Task<IReadOnlyList<UserEntity>> getAll();
        Task<UserEntity> getById(int id);
        Task insert(UserEntity entity);
        Task saveChanges();
        Task update(UserEntity entity);

    }
}
