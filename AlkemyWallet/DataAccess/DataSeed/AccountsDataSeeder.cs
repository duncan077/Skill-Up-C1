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
                        Id = 1, 
                        CreationDate = new DateTime(), 
                        Money = Convert.ToDecimal(100.0), 
                        IsBlocked = true, 
                        UserId = 1,
                        IsDeleted = false
                    },
                    new 
                    { 
                        Id = 2, 
                        CreationDate = new DateTime(), 
                        Money = Convert.ToDecimal(3430.0), 
                        IsBlocked = false, 
                        UserId = 2,
                        IsDeleted = false
                    },
                    new 
                    { 
                        Id = 3, 
                        CreationDate = new DateTime(),
                        Money = Convert.ToDecimal(100.0), 
                        IsBlocked = false, 
                        UserId = 3,
                        IsDeleted = false
                    },
                    new
                    {
                        Id = 4,
                        CreationDate = new DateTime(),
                        Money = Convert.ToDecimal(0.0),
                        IsBlocked = false,
                        UserId = 4,
                        IsDeleted=false

                    }
               );
        }

    }
}

