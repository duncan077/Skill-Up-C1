using AlkemyWallet.DataAccess;
using AlkemyWallet.Entities;
using AlkemyWallet.Repositories.Interfaces;

namespace AlkemyWallet.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ITransactionRepository _transactionRepository;
        private IUserRepository _userRepository;
        private FixedTermDepositRepository _fixedTermDepositRepository;
        private AccountsRepository _accountsRepository;
        private ICatalogueRepository _catalogueRepository;
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
        public ITransactionRepository TransactionRepository
        {
            get
            {
                return _transactionRepository = _transactionRepository ?? new TransactionRepository(_walletDbContext);
            }
        }

        public IUserRepository UserRepository
        {
            get
            {
                return _userRepository = _userRepository ?? new UserRepository(_walletDbContext);
            }
        }


        public AccountsRepository AccountsRepository
        { 
            get 
            { 
                return _accountsRepository = _accountsRepository ?? new AccountsRepository(_walletDbContext); 
            } 
        
        }


        public FixedTermDepositRepository FixedTermDepositRepository
        {
            get
            {
                return _fixedTermDepositRepository = _fixedTermDepositRepository ?? new FixedTermDepositRepository(_walletDbContext);
            }
        }
        

        public ICatalogueRepository CatalogueRepository
        {
            get
            {
                return _catalogueRepository = _catalogueRepository ?? new CatalogueRepository(_walletDbContext);
            }
        }

        public async Task Save()
        {
            await _walletDbContext.SaveChangesAsync();
        }

    }
}
