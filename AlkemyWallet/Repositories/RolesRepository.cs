using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class RolesRepository : GenericRepository<RoleEntity>, IRolesRepository
    {
        private readonly WalletDbContext _context;

        public RolesRepository(WalletDbContext walletDbContext) : base(walletDbContext)
        {
            _context = walletDbContext;
        }

        public async Task<IReadOnlyList<RoleEntity>> getAll(PagesParameters rolesParams)
        {
            try
            {
                var collection = _context.Roles as IQueryable<RoleEntity>;

                collection = collection.Where(a => a.IsDeleted == false);

                return PagedList<RoleEntity>.Create(collection,
                rolesParams.PageNumber,
                rolesParams.PageSize);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }
    }
}
