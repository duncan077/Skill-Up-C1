using AlkemyWallet.Core.Helper;
using AlkemyWallet.Core.Services.ResourceParameters;
using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.Repositories
{
    public class UserRepository : GenericRepository<UserEntity>, IUserRepository
    {

        private readonly WalletDbContext _walletDbContext;
        public UserRepository(WalletDbContext walletDbContext) : base(walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public async Task<PagedList<UserEntity>> getAll(PagesParameters pagesParams)
        {
            try
            {
                var collection = _walletDbContext.Users as IQueryable<UserEntity>;

                collection = collection.Where(a => a.IsDeleted == false);

                return PagedList<UserEntity>.Create(collection,
                pagesParams.PageNumber,
                pagesParams.PageSize);
            }
            catch (Exception err)
            {
                throw new Exception(err.Message);
            }
        }

        public async Task<UserEntity> getByUserName(string userName)
        {
            return await _walletDbContext.Set<UserEntity>().Include(r=>r.Role).Include(a=>a.Accounts).Where(u => u.Email == userName).FirstAsync();
        }
        public async Task<UserEntity> GetById(int id)
        {
            return await _walletDbContext.Set<UserEntity>().Include(r => r.Role).Include(a => a.Accounts).Where(u => u.Id == id).FirstAsync();
        }

    }
}
