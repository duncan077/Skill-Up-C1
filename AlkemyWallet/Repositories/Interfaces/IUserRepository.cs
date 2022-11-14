using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IUserRepository:IGenericRepository<UserEntity>
    {
        Task<UserEntity> getByUserName(string userName);
        Task<PagedList<UserEntity>> getAll(PagesParameters pagesParams);
        Task<UserEntity> GetById(int id);
    }
}