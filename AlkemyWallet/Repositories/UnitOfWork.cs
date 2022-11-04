using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<RoleEntity> _rolesRepository;
        private IGenericRepository<TransactionEntity> _transactionRepository;
        private IGenericRepository<AccountsEntity> _accountsRepository;

        private WalletDbContext _walletDbContext;

        public UnitOfWork(WalletDbContext walletDbContext)
        {
            _walletDbContext = walletDbContext;
        }

        public IGenericRepository<RoleEntity> RolesRepository
        {
            get
            {
                  return _rolesRepository = _rolesRepository ?? new GenericRepository<RoleEntity>(_walletDbContext);
            }
        }
        public IGenericRepository<TransactionEntity> TransactionRepository
        {
            get
            {
                return _transactionRepository = _transactionRepository ?? new GenericRepository<TransactionEntity>(_walletDbContext);
            }
        }

        public IGenericRepository<AccountsEntity> AccountsRepository
        { 
            get 
            { 
                return _accountsRepository ?? new GenericRepository<AccountsEntity>(_walletDbContext); 
            } 
        
        }

        public void Save()
        {
            _walletDbContext.SaveChanges();
        }
    }
}
