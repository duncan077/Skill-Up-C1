using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUserRepository:IGenericRepository<UserEntity>
    {
        Task<UserEntity> getByUserName(string userName);
    }
}