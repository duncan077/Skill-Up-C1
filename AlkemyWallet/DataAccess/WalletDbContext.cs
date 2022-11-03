using AlkemyWallet.DataAccess.DataSeed;
using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess
{
    public class WalletDbContext:DbContext
    {

        public  DbSet<RoleEntity> Roles { get; set; }
        public  DbSet<AccountEntity> Accounts { get; set; }
        public  DbSet<UserEntity> Users { get; set; }

        public WalletDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelbuilder) {

            RoleEntitySeeder.ConfigureMyEntity(modelbuilder);

            base.OnModelCreating(modelbuilder);
        
        }

    }
}
