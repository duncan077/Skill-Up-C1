using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private IGenericRepository<RoleEntity> _rolesRepository;
        private IGenericRepository<TransactionEntity> _transactionRepository;
        private IUserRepository _userRepository;
        private IGenericRepository<FixedTermDepositEntity> _fixedTermDepositRepository;
        private IGenericRepository<AccountsEntity> _accountsRepository;
        private IGenericRepository<CatalogueEntity> _catalogueRepository;

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

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_walletDbContext);
            }
        }


        public IGenericRepository<AccountsEntity> AccountsRepository
        { 
            get 
            { 
                return _accountsRepository = _accountsRepository ?? new GenericRepository<AccountsEntity>(_walletDbContext); 
            } 
        
        }


        public IGenericRepository<FixedTermDepositEntity> FixedTermDepositRepository
        {
            get
            {
                return _fixedTermDepositRepository = _fixedTermDepositRepository ?? new GenericRepository<FixedTermDepositEntity>(_walletDbContext);
            }
        }

        public IGenericRepository<CatalogueEntity> CatalogueRepository
        {
            get
            {
                return _catalogueRepository = _catalogueRepository ?? new GenericRepository<CatalogueEntity>(_walletDbContext);
            }
        }

        public void Save()
        {
            _walletDbContext.SaveChanges();
        }


    }
}
