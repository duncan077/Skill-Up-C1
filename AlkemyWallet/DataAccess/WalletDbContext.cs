using AlkemyWallet.DataAccess.DataSeed;
using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace AlkemyWallet.DataAccess
{

    public class WalletDbContext:DbContext
    {
      public WalletDbContext(DbContextOptions options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            UserDataSeeder.UserDataSeed(modelBuilder);
           CatalogueDataSeeder.CatalogueDataSeed(modelBuilder);
            TransactionDataSeeder.TransactionDataSeed(modelBuilder);
            AccountsDataSeeder.AccountsDataSeed(modelBuilder);
           RoleEntitySeeder.ConfigureMyEntity(modelBuilder);
            FixedTermDepositDataSeeder.FixedTermDepositDataSeed(modelBuilder);

            modelBuilder.Entity<FixedTermDepositEntity>()
                .HasOne<AccountsEntity>(a=>a.Account)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<FixedTermDepositEntity>()
                 .HasOne<UserEntity>(a => a.User)
                 .WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TransactionEntity>()
                .HasOne<AccountsEntity>(a => a.Account)
                .WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<TransactionEntity>()
                 .HasOne<UserEntity>(a => a.User)
                 .WithMany().OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<AccountsEntity>()
                .HasOne(u=>u.User)
                .WithMany(a=>a.Accounts)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<UserEntity> Users { get; set; } = null!;
        public virtual DbSet<AccountsEntity> Accounts { get; set; } = null!;
        public virtual DbSet<FixedTermDepositEntity> FixedTermDeposits { get; set; } = null!;
        public virtual DbSet<RoleEntity> Roles { get; set; } = null!;
        public virtual DbSet<TransactionEntity> Transactions { get; set; } = null!;
        public virtual DbSet<CatalogueEntity> Catalogues { get; set; } = null!;

    }
}

