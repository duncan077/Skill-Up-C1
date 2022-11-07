using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IRolesService
    {
        Task delete(RoleEntity entity);
        Task<IReadOnlyList<RoleEntity>> getAll();
        Task<RoleEntity> getById(int id);
        Task insert(RoleEntity entity);
        Task saveChanges();
        Task update(RoleEntity entity);
    }
}