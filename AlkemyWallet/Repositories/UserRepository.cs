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
        public async Task<UserEntity> getByUserName(string userName)
        {
            return await _walletDbContext.Set<UserEntity>().Include(r=>r.Role).Where(u => u.Email == userName).FirstAsync();
        }
    }
}
