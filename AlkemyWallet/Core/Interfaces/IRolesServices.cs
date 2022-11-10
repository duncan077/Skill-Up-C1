using AlkemyWallet.Core.Services;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Core.Interfaces
{
    public interface IRolesServices : IGenericRepository<RoleEntity>, IRolesRepository
    {
        
    }
}
