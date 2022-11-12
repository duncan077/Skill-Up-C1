using AlkemyWallet.Core.Models.DTO;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IRolesService
    {
        Task delete(RoleEntity entity);
        Task<IReadOnlyList<RoleEntity>> getAll();
        Task<RoleEntity> getById(int id);
        Task<RolesDTO> insert(RolesDTO entity);
        Task saveChanges();
        Task<RolesDTO> update(RoleEntity entity);
        Task<IReadOnlyList<RoleEntity>> getAll(PagesParameters rolesParams);
    }
}