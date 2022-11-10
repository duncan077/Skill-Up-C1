using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IRolesRepository : IGenericRepository<RoleEntity>
    {
        Task<IReadOnlyList<RoleEntity>> getAll(RolesParameters rolesParams);
    }
}
