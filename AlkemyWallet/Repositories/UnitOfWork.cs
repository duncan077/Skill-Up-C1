using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRolesRepository _rolesRepository;
        private WalletDbContext _walletDbContext;

        public UnitOfWork(WalletDbContext walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public IRolesRepository RolesRepository
        {
            get
            {
                  return _rolesRepository = _rolesRepository ?? new RolesRepository(_walletDbContext);
            }
        }


        public void Save()
        {
            _walletDbContext.SaveChanges();
        }
    }
}
