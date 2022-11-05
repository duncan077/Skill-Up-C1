using AlkemyWallet.Entities;
using Microsoft.EntityFrameworkCore;

namespace AlkemyWallet.DataAccess.DataSeed
{
    public static partial class AccountsDataSeeder
    {
        public static void AccountsDataSeed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AccountsEntity>().HasData(
                    new 
                    { 
                        Id = 0, 
                        CreationDate = new DateTime(), 
                        Money = 100.00, 
                        IsBlocked = true, 
                        UserId = 0 
                    },
                    new 
                    { 
                        Id = 1, 
                        CreationDate = new DateTime(), 
                        Money = 0.00, 
                        IsBlocked = true, 
                        UserId = 1 
                    },
                    new 
                    { 
                        Id = 2, 
                        CreationDate = new DateTime(),
                        Money = 100.00, 
                        IsBlocked = false, 
                        UserId = 2 
                    },
                    new
                    {
                        Id = 3,
                        CreationDate = new DateTime(),
                        Money = 0.00,
                        IsBlocked = false,
                        UserId = 3
                    }
               );
        }

    }
}

