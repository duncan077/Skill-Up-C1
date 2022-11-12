using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.Entities;

namespace AlkemyWallet.Repositories.Interfaces
{
    public interface IRolesRepository : IGenericRepository<RoleEntity>
    {
        Task<IReadOnlyList<RoleEntity>> getAll(PagesParameters rolesParams);
    }
}
