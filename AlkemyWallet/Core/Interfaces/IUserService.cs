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
        Task<UserEntity> QueryAsync(Expression<Func<UserEntity, bool>> filter, Func<IQueryable<UserEntity>, IOrderedQueryable<UserEntity>> orderBy = null, Func<IQueryable<UserEntity>, IQueryable<UserEntity>> includes = null);
        Task saveChanges();
        Task update(UserEntity entity);

    }
}
