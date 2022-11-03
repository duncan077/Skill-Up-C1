
using AlkemyWallet.DataAccess.DataSeed;
using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess
{
    public class WalletDbContext :DbContext
    {
        public WalletDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserDataSeeder.UserDataSeed(modelBuilder);
            CatalogueDataSeeder.CatalogueDataSeed(modelBuilder);
        }

        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<AccountsEntity> Accounts { get; set; } = null!;
        public virtual DbSet<FixedTermDeposit> FixedTermDeposits { get; set; } = null!;
        public virtual DbSet<RoleEntity> Roles { get; set; } = null!;
        public virtual DbSet<TransactionEntity> Transactions { get; set; } = null!;
        public virtual DbSet<CatalogueEntity> Catalogues { get; set; } = null!;
    }
}
